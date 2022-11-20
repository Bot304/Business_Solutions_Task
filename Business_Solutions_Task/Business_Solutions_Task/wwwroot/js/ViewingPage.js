function did(id) { return document.getElementById(id); }
var orderListItem;
orderListItem = did('OrderList').outerHTML;
did('OrderList').outerHTML = '';
$(document).ready(async function () {
    GetPage();
    function addRow(OrderItemIdRow, NameRow, QuantityRow, UnitRow, OrderIdRow, NamberRow, DateRow, ProvIdRow, ProvNameRow) {
        $('#table > tbody:last-child').append(orderListItem
            .replace('$OrderItemId', OrderItemIdRow)
            .replace('$Name', NameRow)
            .replace('$Quantity', QuantityRow)
            .replace('$Unit', UnitRow)
            .replace('$OrderId', OrderIdRow)
            .replace('$Namber', NamberRow)
            .replace('$Date', DateRow)
            .replace('$ProvId', ProvIdRow)
            .replace('$ProvName', ProvNameRow)
        );
    }
    // получение заказов
    function GetPage() {
        did('TBody').innerHTML = '';
        $.ajax({
            url: "/Home/GetPage",
            type: "Get",
            success: function (json) {
                $.each(json, function () {

                    addRow(this.id, this.name, this.quantity, this.unit, this.orderId, this.ordN, this.ordD, this.provId, this.provName);

                });
            }
        });
    }

    // фильтр
    $('#ButtonFilter').click(function () {

        var date1 = $('#date1').val()
        var date2 = $('#date2').val()
        var prividerId = $('#PrividerId').val()

        did('TBody').innerHTML = '';
        $.ajax({
            url: "/Home/GetPage",
            data: { 'date1': date1, 'date2': date2, 'prividerId': prividerId },
            type: "Post",
            success: function (json) {
                $.each(json, function () {

                    addRow(this.id, this.name, this.quantity, this.unit, this.orderId, this.ordN, this.ordD, this.provId, this.provName);

                });
            }
        });

    });

    // редактирование заказов
    $(document).on("change", "table#table tbody tr", async function () {

        var thisTr = $(this).closest("tr");
        var orderItemId = thisTr.find('button.RemoveOrder').val();
        var name = thisTr.find('input.Name').val();
        var quantity = thisTr.find('input.Quantity').val();
        var unit = thisTr.find('input.Unit').val();
        var orderId = thisTr.find('input.OrderId').val();
        var namber = thisTr.find('input.Namber').val();
        var date = thisTr.find('input.Date').val();


        $.ajax({
            url: "/Home/UpdatePage",
            data: { 'orderItemId': orderItemId, 'name': name, 'quantity': quantity, 'unit': unit, 'orderId': orderId, 'namber': namber, 'date': date},
            type: "POST",
            success: function () {

                $('table#table tbody tr').each(function () {

                    if ($(this).find('button.RemoveOrder').val() == orderItemId) {

                        $(this).find('input.Name').val(name);
                        $(this).find('input.Quantity').val(quantity);
                        $(this).find('input.Unit').val(unit);
                        $(this).find('input.Namber').val(namber);
                        $(this).find('input.Date').val(date);
                        GetPage();
                    }
                });
            }
        });
    });

    // удалить заказ
    $(document).on("click", "table#table button.RemoveOrder", async function () {
        var orderItemId = $(this).val();
        $.ajax({
            url: "/Home/RemoveOrderItem",
            data: { 'orderItemId': orderItemId },
            type: "POST",
            success: function () {

                $('table#table tbody tr').each(function () {

                    if ($(this).find('button.RemoveOrder').val() == orderItemId) {
                        $(this).remove();
                    }
                });
            }
        });

    });

});