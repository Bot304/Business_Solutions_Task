
$(document).ready(async function () {

    // добавить заказ
    $('#ButtonAddOrder').click(function () {

        var number = $('#Number').val()
        var date = $('#Date').val()
        var prividerId = $('#PrividerId').val()

        $.ajax({
            url: "/Home/AddOrder",
            data: { 'number': number, 'date': date, 'prividerId': prividerId},
            type: "POST",
            success: function (json) {

                $.each(json, function () {
                    var orderId = this.id;
                    var number = this.number;
                    var date = this.date;
                    var prividerId = this.prividerId;
                    });

            }
        });

    });

    // добавить элемент заказа
    $('#ButtonAddOrderItem').click(function () {

        var name = $('#Name').val()
        var quantity = $('#Quantity').val()
        var unit = $('#Unit').val()

        var orderId = $('#OrderId').val()

        $.ajax({
            url: "/Home/AddOrderItem",
            data: { 'name': name, 'quantity': quantity, 'unit': unit, 'orderId': orderId },
            type: "POST",
            success: function (json) {

                $.each(json, function () {
                    var orderId = this.id;
                    var number = this.number;
                    var date = this.date;
                    var prividerId = this.prividerId;
                });

            }
        });

    });

});