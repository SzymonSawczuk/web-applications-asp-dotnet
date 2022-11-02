var canvas_arr = document.getElementsByTagName("canvas");

for (let element of canvas_arr) {
  element.addEventListener("mousemove", display, false);
  element.addEventListener("mouseleave", clear, false);
}

function clear(event, canvas_element) {
  canvas_element = canvas_element == undefined ? this : canvas_element;

  canvas_element
    .getContext("2d")
    .clearRect(0, 0, canvas_element.width, canvas_element.height);
}

function createLine(start_x, start_y, to_x, to_y, ctx) {
  ctx.beginPath();
  ctx.moveTo(start_x, start_y);
  ctx.lineTo(to_x, to_y);
  ctx.strokeStyle = "#34b0f0";
  ctx.lineWidth = 4;
  ctx.stroke();
}

function display(event) {
  let canvas_element = this;
  let ctx = canvas_element.getContext("2d");

  clear(event, canvas_element);

  let mouse_x = event.clientX - canvas_element.offsetLeft + scrollX;
  let mouse_y = event.clientY - canvas_element.offsetTop + scrollY;

  createLine(0, 0, mouse_x, mouse_y, ctx);
  createLine(0, canvas_element.height, mouse_x, mouse_y, ctx);
  createLine(canvas_element.width, 0, mouse_x, mouse_y, ctx);
  createLine(
    canvas_element.width,
    canvas_element.height,
    mouse_x,
    mouse_y,
    ctx
  );
}
