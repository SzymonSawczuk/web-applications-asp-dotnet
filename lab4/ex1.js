var size = 13;
const arr_of_elements = [];
var mult_matrix = document.getElementById("multMatrix");

const getRandomInt = (min, max) =>
  Math.floor(Math.random() * (max - min) + min);

function generateElements(amount) {
  for (let i = 0; i < amount; i++) {
    let new_element = getRandomInt(1, 100);

    while (arr_of_elements.includes(new_element) == true) {
      new_element = getRandomInt(1, 100);
    }

    arr_of_elements.push(new_element);
  }
}

function generateHead() {
  let matrix_head = document.createElement("thead");
  mult_matrix.appendChild(matrix_head);
  matrix_head.appendChild(document.createElement("th"));

  for (let i = 0; i < size; i++) {
    let new_head = document.createElement("th");
    new_head.innerText = arr_of_elements[i];
    matrix_head.appendChild(new_head);
  }
}

function generateBody() {
  let matrix_body = document.createElement("tbody");
  mult_matrix.appendChild(matrix_body);

  for (let i = 0; i < size; i++) {
    let new_head = document.createElement("th");
    new_head.innerText = arr_of_elements[i];

    let new_row = document.createElement("tr");

    matrix_body.appendChild(new_row);
    new_row.appendChild(new_head);

    for (let j = 0; j < size; j++) {
      let new_td = document.createElement("td");
      let new_value = arr_of_elements[i] * arr_of_elements[j];
      new_td.innerText = new_value;
      new_td.className =
        new_value % 3 == 0
          ? "mod3eq0"
          : new_value % 3 == 1
          ? "mod3eq1"
          : "mod3eq2";
      new_row.appendChild(new_td);
    }
  }
}

function main() {
  generateElements(size);
  generateHead();
  generateBody();
}

function askUserAmount() {
  let amount = parseInt(
    window.prompt("Input amount of rows and columns (between 5 and 20)")
  );

  if (isNaN(amount) || amount < 5 || amount > 20)
    window.alert("Wrong input. Amount of rows and colums set on default 13");
  else size = amount;

  main();
}

window.addEventListener("load", askUserAmount, false);
