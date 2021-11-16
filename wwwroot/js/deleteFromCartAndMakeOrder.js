let wrapper = document.querySelector('.product-cart__wrapper');
wrapper.addEventListener('click', async (e) => {
    if (e.target.classList[0] === "cart-item__delete-button") {
        await fetch('/Home/DeleteFromCart', {
            method: 'POST',
            body: JSON.stringify({ OrderItemId: e.target.id }),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        let url = await fetch('/Home/GetCartView', {
            method: 'GET'
        });
        let res = await(await fetch(url.url)).body.getReader().read();
        let str = new TextDecoder("utf-8").decode(res.value);
        let elem = document.querySelector('.product-cart__wrapper');
        let container = document.querySelector('.product-cart__container');
        let scrollTop = container.scrollTop;
        elem.innerHTML = str;
        container = document.querySelector('.product-cart__container');
        container.scrollTo(0, scrollTop);
    }    
    if (e.target.classList[0] === "product-cart__make-order-button") {
        await fetch('/Home/MakeOrder', {
            method: 'POST'
        });
        let url = await fetch('/Home/GetCartView', {
            method: 'GET'
        });
        let res = await (await fetch(url.url)).body.getReader().read();
        let str = new TextDecoder("utf-8").decode(res.value);
        let elem = document.querySelector('.product-cart__wrapper');
        elem.innerHTML = str;
        elem.getElementsByClassName("product-cart__container")[0].innerHTML = " <br />Спасибо за заказ!<br />Статус заказа можно увидеть в личном кабинете<br />"
            + "<br />Хотите сделать новый? Просто добавте товары в корзину";
    }
});