document.addEventListener("DOMContentLoaded", () => {

    const categoryChecks = document.querySelectorAll(".filter-category");
    const colorFilters = document.querySelectorAll(".color-filter");

    const priceSlider = document.getElementById("priceFilter");
    const priceValue = document.getElementById("priceValue");

    const sortSelect = document.getElementById("sortSelect");
    const clearBtn = document.getElementById("clearFilters");

    /* ================== STATE ================== */
    let selectedCategories = [];
    let selectedColors = [];
    let maxPrice = Number(priceSlider.value);

    /* ================== FILTER CORE ================== */
    function applyFilters() {

        const maxPrice = Number(priceSlider.value);

        const activeCategories = [...categoryChecks]
            .filter(c => c.checked)
            .map(c => c.value);

        products.forEach(product => {

            const productPrice = Number(product.dataset.price);
            const productCategory = product.dataset.category;

            const productColors = product.dataset.colors
                ? product.dataset.colors.split(',')
                : [];

            const categoryMatch =
                activeCategories.length === 0 ||
                activeCategories.includes(productCategory);

            const priceMatch = productPrice <= maxPrice;

            const colorMatch =
                selectedColors.length === 0 ||
                selectedColors.some(c => productColors.includes(c));

            product.style.display =
                categoryMatch && priceMatch && colorMatch
                    ? 'flex'
                    : 'none';
        });
    }


    /* ================== CATEGORY ================== */
    categoryChecks.forEach(chk => {
        chk.addEventListener("change", () => {

            selectedCategories = Array.from(categoryChecks)
                .filter(c => c.checked)
                .map(c => c.value);

            applyFilters();
        });
    });

    /* ================== COLOR ================== */
    let selectedColors = [];

    document.querySelectorAll('.color-filter').forEach(filter => {
        filter.addEventListener('click', function () {

            const colorId = this.dataset.color;

            this.classList.toggle('active');

            if (selectedColors.includes(colorId)) {
                selectedColors = selectedColors.filter(c => c !== colorId);
            } else {
                selectedColors.push(colorId);
            }

            applyFilters();
        });
    });


    document.querySelectorAll('.color-filter, .wishlist-btn').forEach(el => {
    el.addEventListener('click', e => {
        e.stopPropagation();   // ⛔ STOP card click
    });
});


    /* ================== PRICE ================== */
    priceSlider.addEventListener("input", () => {
        maxPrice = Number(priceSlider.value);
        priceValue.textContent = maxPrice;
        applyFilters();
    });

    /* ================== SORT ================== */
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

    /* ================== CLEAR ================== */
    clearBtn.addEventListener("click", () => {

        selectedCategories = [];
        selectedColors = [];
        maxPrice = 200;

        categoryChecks.forEach(c => c.checked = false);
        colorFilters.forEach(c => c.classList.remove("active"));

        priceSlider.value = 200;
        priceValue.textContent = 200;

        applyFilters();
    });

    /* ================== WISHLIST ================== */
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

    // ✅ init
    applyFilters();
});
