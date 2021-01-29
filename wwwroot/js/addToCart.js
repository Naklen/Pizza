let containers = document.getElementsByClassName('product-container');
for (let item of containers) {
    let button = item.querySelector('.product-container__in-cart-button');
    let quantity = item.querySelector('.quantity-block__num');
    button.addEventListener('click', () => {
        addToCart(quantity.value, item.id);
    });
}

async function addToCart(quantity, productId) {    
    await fetch('/Home/AddToCart', {
        method: 'POST',
        body: JSON.stringify({ Quantity: quantity, ProductId: productId }),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    let url = await fetch('/Home/GetCartView', {
        method: 'GET'
    });
    let res = await (await fetch(url.url)).body.getReader().read();
    let str = new TextDecoder("utf-8").decode(res.value);
    let elem = document.querySelector('.product-cart__wrapper');
    let container = document.querySelector('.product-cart__container');
    let scrollTop = container.scrollTop;
    elem.innerHTML = str;
    container = document.querySelector('.product-cart__container');
    container.scrollTo(0, scrollTop);
}