$(document).ready(function () {
    //tiếp tục mua hàng
    $('#btn-continue').on('click', function () {
        window.location.href = "/";
        //console.log('test');
    });

    //cập nhập số lượng
    $('#btn-update').on('click', function () {
        var listproduct = $('.txtquatity');
        var cartList = [];
        $.each(listproduct, function (i, item) {
            cartList.push({
                Quatity: $(item).val(),
                Product: {
                    ProductID: $(item).data('id')
                }
            });
            $.ajax({
                type: 'POST',
                data: { cartModel: JSON.stringify(cartList) },
                url: '/Cart/Update',
                success: function (kq) {
                    if (kq.status == true);
                    window.location.href = "/Cart/Index";
                }
            });
        })

    });

    //xóa sản từng sản phẩm
    $(".btn-del").click(function () {
        var id = $(this).attr('id');
        console.log(id);
        $.ajax({
            type: 'POST',
            data: { productId: id},
            url: '/Cart/DeleteCart',
            success: function (kq) {
                $('.cart').html(kq);
            }// tìm class cart thay hết html = html kq partial
        });
    });

    //xóa tất cả sản phẩm
    $("#btn-delete-cart").click(function () {
        $.ajax({
            type: 'POST',
            url: '/Cart/DeleteCartAll',
            success: function (kq) {
                $('.cart').html(kq);
            }
        });
    });

    //thanh toán 
    $('#btn-thanh-toan').click(function () {
        if (confirm('Are you really want to check out ?')) {
            $.ajax({
                type: 'POST',
                url: '/Cart/CheckOut',
                success: function (kq) {
                    alert(kq);
                    window.location.reload(true);
                }
            });
        }
    });
})
