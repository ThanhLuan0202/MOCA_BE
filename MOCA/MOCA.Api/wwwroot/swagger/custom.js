window.onload = function () {
    const ui = SwaggerUIBundle({
        url: "/swagger/v1/swagger.json",
        dom_id: "#swagger-ui",
        presets: [
            SwaggerUIBundle.presets.apis,
        ],
        layout: "BaseLayout",
        onComplete: function () {
            // Ghi đè fetch để tự động lấy token từ login
            const originalFetch = window.fetch;
            window.fetch = function (...args) {
                const [resource, config] = args;

                if (resource.includes("/api/Authen/Login")) {
                    return originalFetch(...args).then(async response => {
                        const clone = response.clone();
                        try {
                            const json = await clone.json();
                            const token = json.token || json.accessToken;

                            if (token) {
                                ui.preauthorizeApiKey("Bearer", "Bearer " + token);
                                console.log("Auto token added!");
                            }
                        } catch (e) {
                            console.error(" Cannot parse token", e);
                        }
                        return response;
                    });
                }

                return originalFetch(...args);
            };
        }
    });
}
