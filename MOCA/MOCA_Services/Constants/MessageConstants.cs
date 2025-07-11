using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Constants

{
    public class MessageConstants
    {
        //role
        public const string ROLE_NOT_EXIST = "Role không tồn tại";
        public const string GET_ROLES_SUCCESS = "Lấy danh sách Role thành công";
        public const string ROLE_UPDATE_SUCCESS = "Cập nhật Role thành công";
        public const string ROLE_DELETE_SUCCESS = "Xóa Role thành công";
        public const string GET_ROLE_EXISTED = "Role đã tồn tại";
        public const string ROLE_CREATE_SUCCESS = "Tạo Role thành công";

        //user
        public const string USER_NOT_EXIST = "Người dùng không tồn tại";
        public const string USER_EXIST = "Tên người dùng đã tồn tại";
        public const string EMAIL_EXIST = "Email đã được sử dụng";
        public const string GET_USER_BY_ID = "Lấy thông tin người dùng thành công";
        public const string GET_MANY_USER_SUCCESS = "Lấy danh sách người dùng thành công";
        public const string USER_DELETE_SUCCESS = "Xóa người dùng thành công";
        public const string USER_UPDATE_SUCCESS = "Cập nhật người dùng thành công";
        public const string USER_CREATE_SUCCESS = "Tạo người dùng thành công";
        public const string USER_CREATE_FAIL = "Tạo người dùng thất bại";

        //profile
        public const string PROFILE_UPDATE_SUCCESS = "Cập nhật thông tin người dùng thành công";
        public const string PROFILE_ADD_AVATAR_SUCCESS = "Thêm ảnh của người dùng thành công";
        public const string PROFILE_ADDRESS_EMPTY = "Không có thông tin địa chỉ để cập nhật.";
        public const string PROFILE_ADDRESS_ADD_SUCCESS = "Thông tin địa chỉ được thêm thành công.";

        //password
        public const string PASSWORD_INVALID = "Mật khẩu không đúng";
        public const string PASSWORD_CHANGE_SUCCESS = "Mật khẩu được thay đổi thành công";
        public const string PASSWORD_NOT_DUPLICATE = "Mật khẩu không được trùng với mật khẩu cũ.";

        //authen
        public const string LOGIN_SUCCESS = "Đăng nhập thành công";
        public const string REGISTER_SUCCESS = "Đăng kí tài khoản thành công";
        public const string CONFIRM_EMAIL_SUCCESS = "Xác minh tài khoản thành công";
        public const string USER_NOT_AUTHORIZED = "Người dùng chưa được xác thực.";
        public const string NEW_ACCESS_CONFIRM = "Cấp mới token thành công";
        public const string REFRESH_NOT_VALID = "Refresh token không hợp lệ";

        //brand
        public const string BRAND_NOT_EXIST = "Thương hiệu không tồn tại";
        public const string GET_BRAND_SUCCESS = "Lấy thông tin thương hiệu thành công";
        public const string BRAND_UPDATE_SUCCESS = "Thương hiệu cập nhật thành công";
        public const string BRAND_DELETE_SUCCESS = "Thương hiệu xóa thành công";
        public const string GET_BRAND_EXISTED = "Thương hiệu đã tồn tại";
        public const string BRAND_CREATE_SUCESS = "Tạo thương hiệu thành công";
        public const string GET_BRAND_BY_ID = "Lấy thương hiệu theo id thành công";
        public const string GET_LIST_BRAND_SUCCESS = "Lấy danh sách thương hiệu thành công";
        public const string BRAND_NAME_EXIST = "Tên thương hiệu đã tồn tại";

        //product 
        public const string PRODUCT_CREATE_SUCESS = "Sản phẩm tạo thành công";
        public const string PRODUCT_CREATE_FAIL = "Sản phẩm tạo thất bại";
        public const string PRODUCT_DELETE_SUCESS = "Sản phẩm xóa thành công";
        public const string PRODUCT_NOT_EXIST = "Sản phẩm không tồn tại";
        public const string GET_LIST_PRODUCT_SUCCESS = "Lấy danh sách sản phẩm thành công";
        public const string GET_PRODUCT_BY_ID = "Lấy sản phẩm theo id thành công";
        public const string PRODUCT_UPDATE_SUCCESS = "Sản phẩm cập nhật thành công";
        public const string PRODUCT_EXIST = "Sản phẩm đã tồn tại";

        //product image
        public const string PRODUCT_IMAGE_CREATE_SUCESS = "Hình ảnh sản phẩm tạo thành công";
        public const string PRODUCT_IMAGE_UPDATE_SUCCESS = "Hình ảnh sản phẩm cập nhập thành công";
        public const string PRODUCT_IMAGE_DELETE_SUCCESS = "Hình ảnh sản phẩm xóa thành công";
        public const string PRODUCT_IMAGE_NOT_EXIST = "Hình ảnh sản phẩm không tồn tại";
        public const string PRODUCT_IMAGE_NOT_BELONG = "Hình ảnh sản phẩm không thuộc về Sản phẩm đã chỉ định.";
        public const string GET_LIST_PRODUCT_IMAGE_SUCCESS = "Lấy danh sách hình ảnh sản phẩm thành công";
        public const string GET_PRODUCT_IMAGE_SUCCCESS = "Lấy hình ảnh sản phẩm theo thành công";

        //category
        public const string CATEGORY_NOT_EXIST = "Danh mục không tồn tại";
        public const string CATEGORY_NAME_EXIST = "Tên danh mục đã tồn tại";
        public const string CATEGORY_CREATE_SUCESS = "Danh mục tạo thành công";
        public const string CATEGORY_DELETE_SUCCESS = "Danh mục xóa thành công";
        public const string GET_LIST_CATEGORY_SUCCESS = "Lấy danh sách danh mục thành công";
        public const string GET_CATEGORY_BY_ID = "Lấy danh mục theo id thành công";
        public const string CATEGORY_UPDATE_SUCCESS = "Danh mục cập nhật thành công";
        public const string PARENT_CATEGORY_NOT_EXIST = "Danh mục gốc không tồn tại";

        //product material
        public const string PRODUCT_MATERIAL_EXIST = "Vật liệu sản phẩm tồn tại";
        public const string PRODUCT_MATERIAL_NOT_EXIST = "Vật liệu sản phẩm không tồn tại";
        public const string PRODUCT_MATERIAL_NAME_EXIST = "Tên Vật liệu sản phẩm đã tồn tại";
        public const string PRODUCT_MATERIAL_CREATE_SUCESS = "Vật liệu sản phẩm tạo thành công";
        public const string PRODUCT_MATERIAL_DELETE_SUCCESS = "Vật liệu sản phẩm xóa thành công";
        public const string GET_LIST_PRODUCT_MATERIAL_SUCCESS = "Lấy danh sách Vật liệu sản phẩm thành công";
        public const string GET_PRODUCT_MATERIAL_BY_ID = "Lấy Vật liệu sản phẩm theo id thành công";
        public const string PRODUCT_MATERIAL_UPDATE_SUCCESS = "Vật liệu sản phẩm cập nhật thành công";
        public const string PARENT_PRODUCT_MATERIAL_NOT_EXIST = "Vật liệu sản phẩm gốc không tồn tại";

        //blog
        public const string BLOG_CREATE_SUCESS = "Bài viết được tạo thành công";
        public const string BLOG_UPDATE_SUCCESS = "Bài viết được cập nhập thành công";
        public const string BLOG_DELETE_SUCCESS = "Bài viết được xóa thành công";
        public const string BLOG_NOT_EXIST = "Bài viết không tồn tại";
        public const string BLOG_DATA_NOT_EMPTY = "Các thông tin bài viết không được trống";
        public const string BLOG_NOT_YOUR_BLOG = "Người dùng không có quyền thay đổi bài viết này.";
        public const string BLOG_GET_SUCCESS = "Lấy bài viết thành công";
        public const string GET_LIST_BLOG_SUCCESS = "Lấy danh sách bài viết thành công.";

        //feedback
        public const string FEEDBACK_NOT_EXIST = "Phản hồi không tồn tại";
        public const string FEEDBACK_CREATE_SUCESS = "Phản hồi tạo thành công";
        public const string FEEDBACK_UPDATE_SUCCESS = "Phản hồi cập nhật thành công";
        public const string FEEDBACK_DELETE_SUCCESS = "Phản hồi xóa thành công";
        public const string USER_NOT_PURCHASE_PRODUCT = "Người dùng chưa mua sản phẩm này";
        public const string FEEDBACK_EXIST = "Bạn đã đánh giá sản phẩm này rồi.";
        public const string GET_LIST_FEEDBACK_SUCCESS = "lấy danh sách đánh giá thành công";
        public const string GET__FEEDBACK_SUCCESS = "lấy đánh giá thành công";

        //cart
        public const string ADD_TO_CART_SUCESS = "Thêm vào giỏ hàng thành công";
        public const string UPDATE_TO_CART_SUCESS = "Chỉnh sửa vào giỏ hàng thành công";
        public const string NOT_FOUND_CART = "Không tìm thấy giỏ hàng của user";
        public const string DELETE_CART_SUCCESS = "Đã xóa hết giỏ hàng của user";
        public const string GET_CART_SUCCESS = "Lấy thông tin giỏ hàng thành công";
        public const string REMOVE_ITEM_SUCCESS = "Xóa sản phẩm trong giỏ hàng thành công";
        public const string DECREASE_QUANTITY_SUCCESS = "Đã giảm số lượng sản phẩm trong giỏ hàng.";


        //wishlist
        public const string WISHLIST_PRODUCT_EXIST = "Sản phẩm đã tồn tại trong wishlist";
        public const string WISHLIST_NOT_EXIST = "Wishlist không tồn tại";
        public const string WISHLIST_ADD_SUCCESS = "Wishlist được thêm thành công";
        public const string WISHLIST_DELETE_SUCCESS = "Wishlist được xóa thành công";
        public const string WISHLIST_GET_SUCCESS = "Lấy danh sách wishlist thành công";
        public const string WISHLIST_ADD_TO_CART_SUCCESS = "Thêm wishlist vào cart thành công";
        public const string WISHLIST_ADD_TO_CART_FAIL = "Thêm wishlist vào cart thất bại";

        //voucher
        public const string VOUCHER_NOT_EXIST = "Mã giảm giá không tồn tại";
        public const string VOUCHER_TYPE_NOT_EXIST = "Loại mã giảm giá không hợp lệ.";
        public const string VOUCHER_CREATE_SUCCESS = "Mã giảm giá tạo thành công";
        public const string VOUCHER_UPDATE_SUCCESS = "Mã giảm giá cập nhật thành công";
        public const string VOUCHER_DELETE_SUCCESS = "Mã giảm giá xóa thành công";
        public const string VOUCHER_GET_SUCCESS = "Danh sách mã giảm giá lấy thành công";
        public const string VOUCHER_CAN_USE = "Mã giảm giá có thể được sử dụng";
        public const string VOUCHER_CANNOT_USE = "Mã giảm giá không thể được sử dụng";
        public const string VOUCHER_ACTIVATE_SUCCESS = "Kích hoạt mã giảm giá thành công";
        public const string VOUCHER_ALREADY_USED = "Đơn hàng đã có mã giảm giá, không thể thêm";

        //order
        public const string ORDER_NOT_EXIST = "Đơn hàng không tồn tại";
        public const string ORDER_CREATE_SUCCESS = "Tạo đơn hàng thành công";
        public const string ORDER_NOT_FOUND = "Đơn hàng không tìm thấy";
        public const string GET_LIST_ORDER_SUCCESS = "Lấy danh sách sản phẩm thành công";
        public const string ORDER_FOUND = "Lấy đơn hàng thành công";
        public const string ORDER_UPDATE_SUCCESS = "Cập nhật đơn hàng thành công";
        public const string CONFIRM_ORDER_SUCESS = "Xác thực đơn hàng thành công";

        //payment
        public const string PAYMENT_DESCRIPTION = "Đơn hàng ";
        public const string PAYMENT_NOT_FOUND = "Không tìm thấy đơn hàng";
        public const string PAYMENT_NOT_FOUND_ORDER = "Không tìm thấy thanh toán cho đơn hàng này";

        //login google
        public const string TOKEN_NOT_VALID = "Token hết hạn";
        public const string USER_HAS_BEEN_DELETE = "User không tồn tại";
        public const string LOGIN_SUCCESS_MESSAGE = "Login Thành Công";

        //format
        public const string WRONG_DATE_FORMAT = "Sai định dạng ngày tháng";
        public const string DATE_NOT_EXISTED = "Ngày tháng không hợp lệ hoặc không tồn tại";
        public const string DATE_NOT_VALID_FROM_TO = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc";
        public const string PHONE_EXIST = "Số điện thoại đã tồn tại!";

        //data 
        public const string WEBHOOK_ERROR = "Webhook không hợp lệ hoặc không xác thực được.";
    }
}
