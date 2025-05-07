document.addEventListener('DOMContentLoaded', function () {
    // Get all hotel cards
    const hotelCards = document.querySelectorAll('.hotel-card');
    const searchInput = document.getElementById('search-input');
    const priceSlider = document.getElementById('price-slider');
    const priceDisplay = document.getElementById('price-display');
    const amenityCheckboxes = document.querySelectorAll('.amenity-checkbox');
    const applyFiltersBtn = document.getElementById('apply-filters');
    const resetBtn = document.querySelector('.reset-btn');
    const resultsCount = document.querySelector('.results-count');

    // Initialize price slider
    priceSlider.addEventListener('input', function () {
        priceDisplay.textContent = '$' + this.value;
    });

    // Apply filters function
    function applyFilters() {
        const searchTerm = searchInput.value.toLowerCase();
        const maxPrice = parseInt(priceSlider.value);
        const selectedAmenities = Array.from(amenityCheckboxes)
            .filter(checkbox => checkbox.checked)
            .map(checkbox => checkbox.value);

        let visibleCount = 0;

        hotelCards.forEach(card => {
            const hotelName = card.querySelector('.hotel-name').textContent.toLowerCase();
            const hotelLocation = card.getAttribute('data-location').toLowerCase();
            const hotelPrice = parseInt(card.getAttribute('data-price'));
            const hotelAmenities = card.getAttribute('data-amenities').split(',');

            // Check search term
            const matchesSearch = searchTerm === '' ||
                hotelName.includes(searchTerm) ||
                hotelLocation.includes(searchTerm);

            // Check price
            const matchesPrice = hotelPrice <= maxPrice;

            // Check amenities
            const matchesAmenities = selectedAmenities.length === 0 ||
                selectedAmenities.every(amenity => hotelAmenities.includes(amenity));

            // Show/hide card based on filters
            if (matchesSearch && matchesPrice && matchesAmenities) {
                card.style.display = 'block';
                visibleCount++;
            } else {
                card.style.display = 'none';
            }
        });

        // Update results count
        resultsCount.textContent = `Showing ${visibleCount} hotels`;
    }

    // Reset all filters
    function resetFilters() {
        searchInput.value = '';
        priceSlider.value = 250;
        priceDisplay.textContent = '$250';
        amenityCheckboxes.forEach(checkbox => checkbox.checked = false);
        applyFilters();
    }

    // Event listeners
    applyFiltersBtn.addEventListener('click', applyFilters);
    resetBtn.addEventListener('click', resetFilters);

    // Apply filters when search input changes (with debounce)
    let searchTimeout;
    searchInput.addEventListener('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(applyFilters, 300);
    });

    // Apply filters when price slider changes
    priceSlider.addEventListener('change', applyFilters);

    // Apply filters when amenities change
    amenityCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', applyFilters);
    });

    // Initialize filters on page load
    applyFilters();
});