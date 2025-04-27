document.addEventListener("DOMContentLoaded", function () {
    const toolTypeSelect = document.getElementById("toolType");

    function updateFields() {
        const selectedType = toolTypeSelect.value.toLowerCase();

        // Lista wszystkich klas do ukrycia
        const allTypes = ["drill", "tap", "mill", "special"];

        allTypes.forEach(type => {
            const elements = document.querySelectorAll(`.${type}-only`);
            elements.forEach(el => el.style.display = "none");
        });

        // Pokazanie tylko odpowiednich
        const visibleElements = document.querySelectorAll(`.${selectedType}-only`);
        visibleElements.forEach(el => el.style.display = "block");
    }

    // Inicjalizacja na starcie
    updateFields();

    // Zmiana typu narzędzia
    toolTypeSelect.addEventListener("change", updateFields);
});