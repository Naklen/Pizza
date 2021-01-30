let pizzaButton = document.getElementById('pizza-button');
pizzaButton.addEventListener('click', () => {
    window.scrollTo({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
});

let drinkButton = document.getElementById('drink-button');
drinkButton.addEventListener('click', () => {
    let elem = document.querySelector('.products-container__drink');
    elem.scrollIntoView({ behavior: "smooth", block: "start" }); 
});

let snackButton = document.getElementById('snack-button');
snackButton.addEventListener('click', () => {
    let elem = document.querySelector('.products-container__snack');
    elem.scrollIntoView({ behavior: "smooth", block: "start" });
});

let saladButton = document.getElementById('salad-button');
saladButton.addEventListener('click', () => {
    let elem = document.querySelector('.products-container__salad');
    elem.scrollIntoView({ behavior: "smooth", block: "start" });
});

let komboButton = document.getElementById('kombo-button');
komboButton.addEventListener('click', () => {
    let elem = document.querySelector('.products-container__kombo');
    elem.scrollIntoView({ behavior: "smooth", block: "start" });
});