document.addEventListener("DOMContentLoaded", function () {

    const products = Array.from(document.querySelectorAll(".product-card"));
    const categoryChecks = document.querySelectorAll(".filter-category");
    const colorButtons = document.querySelectorAll(".color-filter");
    const priceSlider = document.getElementById("priceFilter");
    const priceValue = document.getElementById("priceValue");
    const clearBtn = document.getElementById("clearFilters");

    // state (SAFE – scoped)
    let activeCategories = [];
    let activeColors = [];
    let maxPrice = priceSlider ? Number(priceSlider.value) : Infinity;

    function applyFilters() {
        products.forEach(product => {
            const price = Number(product.dataset.price);
            const category = product.dataset.category;
            const colors = product.dataset.colors
                ? product.dataset.colors.split(",")
                : [];

            const categoryMatch =
                activeCategories.length === 0 ||
                activeCategories.includes(category);

            const colorMatch =
                activeColors.length === 0 ||
                activeColors.some(c => colors.includes(c));

            const priceMatch = price <= maxPrice;

            product.style.display =
                categoryMatch && colorMatch && priceMatch
                    ? "flex"
                    : "none";
        });
    }

    // CATEGORY FILTER
    categoryChecks.forEach(chk => {
        chk.addEventListener("change", () => {
            activeCategories = Array.from(categoryChecks)
                .filter(c => c.checked)
                .map(c => c.value);

            applyFilters();
        });
    });

    // COLOR FILTER
    colorButtons.forEach(btn => {
        btn.addEventListener("click", () => {
            const colorId = btn.dataset.color;
            btn.classList.toggle("active");

            if (activeColors.includes(colorId)) {
                activeColors = activeColors.filter(c => c !== colorId);
            } else {
                activeColors.push(colorId);
            }

            applyFilters();
        });
    });

    // PRICE FILTER
    if (priceSlider) {
        priceSlider.addEventListener("input", () => {
            maxPrice = Number(priceSlider.value);
            if (priceValue) priceValue.textContent = maxPrice;
            applyFilters();
        });
    }

    // CLEAR FILTERS
    if (clearBtn) {
        clearBtn.addEventListener("click", () => {
            activeCategories = [];
            activeColors = [];
            maxPrice = priceSlider ? Number(priceSlider.max) : Infinity;

            categoryChecks.forEach(c => c.checked = false);
            colorButtons.forEach(c => c.classList.remove("active"));

            if (priceSlider) priceSlider.value = priceSlider.max;
            if (priceValue) priceValue.textContent = priceSlider.max;

            applyFilters();
        });
    }

    // INIT
    applyFilters();
});

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
