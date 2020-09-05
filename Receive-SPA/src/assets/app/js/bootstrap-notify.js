function notifySuccess(message) {
  var e = { message: message };
  var t = $.notify(e, {
    type: "success",
    allow_dismiss: true,
    newest_on_top: true,
    timer: 2000,
    placement: { from: "top", align: "right" },
    offset: { x: "30", y: "30" },
    delay: 1000,
    z_index: 10000,
    animate: { enter: "animated bounce", exit: "animated bounce" }
  });
}

function notifyError(message) {
  var e = { message: message };
  var t = $.notify(e, {
    type: "danger",
    allow_dismiss: true,
    newest_on_top: true,
    timer: 2000,
    placement: { from: "top", align: "right" },
    offset: { x: "30", y: "30" },
    delay: 1000,
    z_index: 10000,
    animate: { enter: "animated swing", exit: "animated bounce" }
  });
}

function sortTable() {
  $("#table-detail").tableDnD();
}

Array.prototype.move = function (from, to) {
  this.splice(to, 0, this.splice(from, 1)[0]);
  return this;
};




