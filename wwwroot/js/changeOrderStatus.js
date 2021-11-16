let orderList = document.querySelector('.order-list');
orderList.addEventListener('click', async (e) => {
    if (e.target.classList[0] === "order__take-order") {
        onClick(e.target.id, 3);
    }
    if (e.target.classList[0] === "order__send-order") {
        onClick(e.target.id, 4);
    }
    if (e.target.classList[0] === "order__complete-delivery") {
        onClick(e.target.id, 5);
    }
});

async function onClick(orderId, newStatus) {
    await fetch('/Manager/ChangeOrderStatus', {
        method: 'POST',
        body: JSON.stringify({ OrderId: orderId, NewStatus: newStatus }),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    let url = await fetch('/Manager/GetOrderPanelView', {
        method: 'GET'
    });
    let res = await(await fetch(url.url)).body.getReader().read();
    let str = new TextDecoder("utf-8").decode(res.value);
    orderList.innerHTML = str;
}