(function quantityProducts() {
    let productContainers = document.getElementsByClassName("product-container");
    for (let container of productContainers) {
        let qBlock = container.querySelector(".quantity-block");
        let quantityArrowMinus = qBlock.querySelector(".quantity-block__arrow-minus");
        let quantityArrowPlus = qBlock.querySelector(".quantity-block__arrow-plus");
        let quantityNum = qBlock.querySelector(".quantity-block__num");
        quantityArrowMinus.addEventListener('click', quantityMinus);
        quantityArrowPlus.addEventListener('click', quantityPlus);
        function quantityMinus() {
            console.log(quantityNum)
            if (+quantityNum.value > 1) {
                quantityNum.value = (+quantityNum.value - 1);
            }
        }
        function quantityPlus() {
            console.log(quantityNum)
            quantityNum.value = (+quantityNum.value + 1);
        }
        
        let inCartButton = container.querySelector(".product-container__in-cart-button");
        inCartButton.addEventListener('click', () => {
            console.log(`В корзину добавлен товар ${container.id} в количестве ${quantityNum.value}`);
        });
    }    
})();