document.body.addEventListener('htmx:responseError', function(evt) {
  console.error(evt);
  alert(evt.detail.xhr.response);
});
