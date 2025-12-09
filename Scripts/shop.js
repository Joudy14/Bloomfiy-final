document.addEventListener("DOMContentLoaded", () => {

    const products = [...document.querySelectorAll(".product-card")];

    const categoryChecks = document.querySelectorAll(".filter-category");
    const colorFilters = document.querySelectorAll(".color-filter");

    const priceSlider = document.getElementById("priceFilter");
    const priceValue = document.getElementById("priceValue");

    const sortSelect = document.getElementById("sortSelect");
    const clearBtn = document.getElementById("clearFilters");

    let selectedColor = null;

    /* ===================== FILTER CORE ===================== */
    function applyFilters() {

        const maxPrice = Number(priceSlider.value);

        const activeCategories = [...categoryChecks]
            .filter(c => c.checked)
            .map(c => c.value);

        products.forEach(product => {

            const productPrice = Number(product.dataset.price);
            const productCategory = product.dataset.category;
            const productColors = product.dataset.colors
                ? product.dataset.colors.split(",")
                : [];

            /* CATEGORY */
            const categoryMatch =
                activeCategories.length === 0 ||
                activeCategories.includes(productCategory);

            /* PRICE */
            const priceMatch = productPrice <= maxPrice;

            /* COLOR */
            const colorMatch =
                !selectedColor ||
                productColors.includes(selectedColor);

            /* FINAL DECISION */
            if (categoryMatch && priceMatch && colorMatch) {
                product.style.display = "flex";
            } else {
                product.style.display = "none";
            }
        });
    }

    /* ===================== CATEGORY ===================== */
    categoryChecks.forEach(chk =>
        chk.addEventListener("change", applyFilters)
    );

    /* ===================== PRICE ===================== */
    priceSlider.addEventListener("input", () => {
        priceValue.textContent = priceSlider.value;
        applyFilters();
    });

    $(function () {

        let selectedColors = [];

        // ✅ Color filter click
        $('.color-filter').on('click', function () {

            const colorId = parseInt($(this).data('color'));

            $(this).toggleClass('active');

            if (selectedColors.includes(colorId)) {
                selectedColors = selectedColors.filter(c => c !== colorId);
            } else {
                selectedColors.push(colorId);
            }

            applyFilters();
        });


        function applyFilters() {

            $('.product-card').each(function () {

                const colorAttr = $(this).data('colors');

                // ⚠️ safety check
                if (!colorAttr) {
                    $(this).hide();
                    return;
                }

                const productColors = colorAttr
                    .toString()
                    .split(',')
                    .map(Number);

                let show = true;

                // ✅ Color filter logic
                if (selectedColors.length > 0) {
                    show = selectedColors.some(c => productColors.includes(c));
                }

                $(this).toggle(show);
            });
        }

    });


    /* ===================== SORT ===================== */
    sortSelect.addEventListener("change", () => {

        const grid = document.getElementById("productsGrid");
        let sorted = [...products];

        if (sortSelect.value === "name-asc") {
            sorted.sort((a, b) =>
                a.querySelector("h3").innerText.localeCompare(
                    b.querySelector("h3").innerText
                )
            );
        }

        if (sortSelect.value === "price-asc") {
            sorted.sort((a, b) => a.dataset.price - b.dataset.price);
        }

        if (sortSelect.value === "price-desc") {
            sorted.sort((a, b) => b.dataset.price - a.dataset.price);
        }

        grid.innerHTML = "";
        sorted.forEach(p => grid.appendChild(p));
    });

    /* ===================== CLEAR ===================== */
    clearBtn.addEventListener("click", () => {

        categoryChecks.forEach(c => c.checked = false);
        colorFilters.forEach(c => c.classList.remove("active"));

        selectedColor = null;

        priceSlider.value = 200;
        priceValue.textContent = 200;

        applyFilters();
    });

    /* ===================== WISHLIST ===================== */
    document.querySelectorAll(".wishlist-btn").forEach(btn => {

        btn.addEventListener("click", e => {
            e.stopPropagation();

            btn.classList.toggle("active");

            let wishlist = JSON.parse(localStorage.getItem("wishlist") || "[]");
            const id = btn.dataset.id;

            if (btn.classList.contains("active")) {
                if (!wishlist.includes(id)) wishlist.push(id);
            } else {
                wishlist = wishlist.filter(x => x !== id);
            }

            localStorage.setItem("wishlist", JSON.stringify(wishlist));
        });
    });

});
