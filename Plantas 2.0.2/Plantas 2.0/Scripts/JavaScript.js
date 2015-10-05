$(function () {
    $('.confirm').click(function () {
        return window.confirm("Are you sure?");
    });
});

$(function () {
    $('.container').find('img').each(function () {
        var imgClass = (this.width / this.height > 1) ? 'wide' : 'tall';
        $(this).addClass(imgClass);
    });
});
