$(document).ready(function () {

    function applyAjaxFilters() {

        const categories = $('.filter-category:checked')
            .map(function () { return this.value; })
            .get();

        const colors = $('.color-filter.active')
            .map(function () { return $(this).data('color'); })
            .get();

        const maxPrice = $('#priceFilter').val();
        const sort = $('#sortSelect').val();
        const search = $('#searchInput').val();

        $.ajax({
            url: '/Product/Filter',
            type: 'GET',
            traditional: true,
            data: {
                categories: categories,
                colors: colors,
                maxPrice: maxPrice,
                sort: sort,
                search: search
            },
            success: function (html) {
                $('#productsGrid').html(html);
            },
        });
    }

    // CATEGORY
    $('.filter-category').on('change', applyAjaxFilters);

    // COLORS (delegation so it survives AJAX)
    $(document).on('click', '.color-filter', function () {
        $(this).toggleClass('active');
        applyAjaxFilters();
    });

    // PRICE
    $('#priceFilter').on('input', function () {
        $('#priceValue').text(this.value);
        applyAjaxFilters();
    });

    // SORT
    $('#sortSelect').on('change', applyAjaxFilters);

    // TEXT SEARCH
    $('#searchInput').on('keyup', applyAjaxFilters);

    // CLEAR
    $('#clearFilters').on('click', function () {
        $('.filter-category').prop('checked', false);
        $('.color-filter').removeClass('active');
        $('#priceFilter').val($('#priceFilter').attr('max'));
        $('#priceValue').text($('#priceFilter').attr('max'));
        $('#searchInput').val('');
        applyAjaxFilters();
    });

});


document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll('.wishlist-form').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            fetch('/Wishlist/AddAjax', {
                method: 'POST',
                body: new FormData(this)
            })
                .then(r => r.json())
                .then(res => {
                    if (res.success) {
                        this.querySelector('.wishlist-btn')
                            .classList.toggle('active');
                    }
                });
        });
    });

});
