let orderList = document.querySelector('.order-list');
orderList.addEventListener('click', async (e) => {
    if (e.target.classList[0] === "order__delete-button") {
        await fetch('/Account/DeleteOrder', {
            method: 'POST',
            body: JSON.stringify({ OrderId: e.target.id }),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        let url = await fetch('/Account/GetOrderListView', {
            method: 'GET'
        });
        let res = await(await fetch(url.url)).body.getReader().read();
        let str = new TextDecoder("utf-8").decode(res.value);
        //let elem = document.querySelector('..order-list');
        //let container = document.querySelector('.product-cart__container');
        //let scrollTop = container.scrollTop;
        orderList.innerHTML = str;
        //container = document.querySelector('.product-cart__container');
        //container.scrollTo(0, scrollTop);
        //@order.CloseDateTime.Day.@order.CloseDateTime.Month.@order.CloseDateTime.Year
        //- @order.CloseDateTime.Hour: @order.CloseDateTime.Minute
    }
});