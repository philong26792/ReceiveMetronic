declare let $: any;
 //#region menu narvar on click
export function scriptMain() {
  $('.m-menu__item').on('click', function (event) {
    let test = $(this).find('.m-menu__submenu').css('display');
    if (test === 'none') {
      // $('.m-menu__submenu').css('display', 'none');
      $(this).find('.m-menu__submenu').show('slow');
    } else {
      // $('.m-menu__submenu').css('display', 'none');
      $(this).find('.m-menu__submenu').hide('slow');
    }
  });
}

 //#endregion menu narvar on click

//#region notify
export function notifySuccess(message) {
  var e = { message: message };
  var t = $.notify(e, {
    type: 'success',
    allow_dismiss: true,
    newest_on_top: true,
    timer: 2000,
    placement: { from: 'top', align: 'right' },
    offset: { x: '30', y: '30' },
    delay: 1000,
    z_index: 10000,
    animate: { enter: 'animated bounce', exit: 'animated bounce' },
  });
}
export function notifyError(message) {
  var e = { message: message };
  var t = $.notify(e, {
    type: 'danger',
    allow_dismiss: true,
    newest_on_top: true,
    timer: 2000,
    placement: { from: 'top', align: 'right' },
    offset: { x: '30', y: '30' },
    delay: 1000,
    z_index: 10000,
    animate: { enter: 'animated swing', exit: 'animated bounce' },
  });
}

//#endregion notify

//#region loading
export function showLoading() {
  $("#main-loading").show();
}
export function hideLoading() {
  $("#main-loading").hide();
}

//#endregion loading
