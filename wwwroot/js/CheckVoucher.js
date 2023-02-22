$(document).ready(function(){

    var CheckVoucher = function(){
        $('#voucher').css('display', 'none')
        voucherCode = $('.voucher-code').val().toUpperCase()
        if (voucherCode == "")
            return
                
        $.ajax({
            cache: false,
            url: '/Voucher/CheckVoucher',
            type: 'POST',
            data: { VoucherCode: voucherCode },
            success: function (data) {
                switch (data) {
                    case "Không tìm thấy Voucher!":
                        alert("Không tìm thấy Voucher!")
                        break
                    case "Voucher đã hết hạn/lượt sử dụng!":
                        alert("Voucher đã hết hạn/lượt sử dụng")
                        break
                    default:
                        voucher = $('#voucher')
                        voucher.html(data)
                        voucher.css('display', 'block')
                        break
                }
            }
        })
    }

    $('#voucher-code').change(function(){
        CheckVoucher()
    })

    $('.btn-check-voucher').click(function(){
        CheckVoucher()
    })

    $('.btn-apply-voucher').click(function(){

    })
})