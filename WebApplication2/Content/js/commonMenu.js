jQuery(document).ready(function () {
    loadMenu();
});
function loadMenu() {
    jQuery(document).ready(function () {
        jQuery("#menuLoad").load("/menu");
    })
}
