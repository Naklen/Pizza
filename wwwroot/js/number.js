(function quantityProducts() {
    var $quantityArrowMinus = document.querySelector(".quantity-arrow-minus");
    var $quantityArrowPlus = document.querySelector(".quantity-arrow-plus");
    var $quantityNum = document.querySelector(".quantity-num");

    $quantityArrowMinus.addEventListener('click', quantityMinus);
    $quantityArrowPlus.addEventListener('click', quantityPlus);

    function quantityMinus() {
        console.log($quantityNum)
        if (+$quantityNum.value > 1) {
            $quantityNum.value = (+$quantityNum.value - 1);
        }
    }

    function quantityPlus() {
        console.log($quantityNum)
        $quantityNum.value = (+$quantityNum.value + 1);
    }
})();