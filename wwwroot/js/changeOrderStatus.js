const orderList = document.querySelector('.order-list');
let button = document.querySelector('.change-status-button');
button.addEventListener('click', async (e) => {
    e.preventDefault;
    let newStatus;    
    switch (e.target.classList[0]) {
        case "order__take-order":
            newStatus = 3;
            break;
        case "order__send-order":
            newStatus = 4;
            break;
        case "order__complete-delivery":
            newStatus = 5;
            break;
    }   
    await onClick(e.target.id, newStatus);
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