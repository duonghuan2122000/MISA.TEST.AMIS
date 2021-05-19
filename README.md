# Misa Amis 
Misa Amis là bản dựng thử chức năng quản lý nhân viên bao gồm các thao tác cơ bản như thêm, sửa, xóa hay lọc danh sách nhân viên.

## Các công nghệ và thư viện sử dụng
### Server
- ASP.NET CORE 5.0
- Dapper
- MySqlConnector
- MariaDb
- Visual Studio 2019
### UI
- vue/cli
- Vuejs 2.*
- vue-router
- Axios
- dayjs
- vue-date-pick

## Các chức năng hoạt động

 - Hiển thị danh sách nhân viên có phân trang
 - Lọc danh sách nhân viên theo mã hoặc tên nhân viên
 - Thêm, sửa và xóa nhân viên
 - Validate dữ liệu (tooltip + dialog thông báo) ở UI và trên server
 - Hot Key trong Dialog thêm, sửa nhân viên trên UI (Ctrl + s, Ctrl + Shift + s, ESC)

## Khởi chạy 
### Server
1. Sử dụng VS 2019 open `~/Api/MISA.AMIS.sln`
2. Build resolution
3. Running resolution, project sẽ chạy ở URL [https://localhost:44319/swagger/index.html](https://localhost:44319/swagger/index.html)

### UI
1. Truy cập folder `~/vue-ui-2`
2. Tiến hành cài đặt package cho project `npm install`
3. Chạy `npm run serve`, server sẽ run ở development [http://localhost:8080/](http://localhost:8080/)

