document.addEventListener("DOMContentLoaded", () => {

    const mainImage = document.getElementById("pdMainImage");
    const priceDisplay = document.getElementById("pdPrice");

    document.querySelectorAll(".pd-color-option").forEach(btn => {

        btn.addEventListener("click", () => {

            document
                .querySelectorAll(".pd-color-option")
                .forEach(b => b.classList.remove("active"));

            btn.classList.add("active");

            if (btn.dataset.image) {
                mainImage.src = btn.dataset.image;
            }

            if (btn.dataset.price) {
                priceDisplay.textContent = "$" + btn.dataset.price;
            }
        });
    });
});
