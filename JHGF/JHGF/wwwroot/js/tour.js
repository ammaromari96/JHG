document.addEventListener('DOMContentLoaded', function () {
    // Price slider functionality
    const priceSlider = document.getElementById('price-slider');
    const priceValue = document.querySelector('.price-range-value');

    priceSlider.addEventListener('input', function () {
        priceValue.textContent = `$${this.value}`;
    });

    // Filter functionality
    const applyBtn = document.querySelector('.apply-btn');
    const resetBtn = document.querySelector('.reset-btn');
    const tourCards = document.querySelectorAll('.tour-card');

    applyBtn.addEventListener('click', function () {
        const destinationFilter = document.getElementById('destination-filter').value;
        const typeFilter = document.getElementById('type-filter').value;
        const priceFilter = parseInt(priceSlider.value);
        const durationCheckboxes = document.querySelectorAll('input[name="duration"]:checked');
        const featureCheckboxes = document.querySelectorAll('input[name="features"]:checked');

        let visibleTours = 0;

        tourCards.forEach(card => {
            const cardDestination = card.getAttribute('data-destination');
            const cardType = card.getAttribute('data-type').split(',');
            const cardPrice = parseInt(card.getAttribute('data-price'));
            const cardDuration = card.getAttribute('data-duration');
            const cardFeatures = card.getAttribute('data-features').split(',');

            // Check destination filter
            const destinationMatch = !destinationFilter ||
                (cardDestination.includes(destinationFilter) ||
                    (destinationFilter === '' && cardDestination);

            // Check type filter
            const typeMatch = !typeFilter ||
                (cardType.includes(typeFilter)) ||
                (typeFilter === '' && cardType);

            // Check price filter
            const priceMatch = cardPrice <= priceFilter;

            // Check duration filter
            let durationMatch = durationCheckboxes.length === 0;
            durationCheckboxes.forEach(checkbox => {
                const durationValue = checkbox.value;
                if (durationValue === '1' && cardDuration === '1') {
                    durationMatch = true;
                } else if (durationValue === '2' && cardDuration === '2') {
                    durationMatch = true;
                } else if (durationValue === '3' && (cardDuration === '3' || cardDuration === '4' || cardDuration === '5')) {
                    durationMatch = true;
                } else if (durationValue === '7' && parseInt(cardDuration) >= 7) {
                    durationMatch = true;
                }
            });

            // Check features filter
            let featuresMatch = featureCheckboxes.length === 0;
            if (!featuresMatch) {
                const requiredFeatures = Array.from(featureCheckboxes).map(cb => cb.value);
                featuresMatch = requiredFeatures.every(feature =>
                    cardFeatures.includes(feature)
                );
            }

            // Show/hide card based on filters
            if (destinationMatch && typeMatch && priceMatch && durationMatch && featuresMatch) {
                card.style.display = 'block';
                visibleTours++;
            } else {
                card.style.display = 'none';
            }
        });

        // Update results count
        document.querySelector('.results-count').textContent = `Showing ${visibleTours} of 18 tours`;

        // Show no results message if no tours match
        const noResults = document.querySelector('.no-results');
        if (visibleTours === 0) {
            if (!noResults) {
                const tourGrid = document.querySelector('.tour-grid');
                const noResultsDiv = document.createElement('div');
                noResultsDiv.className = 'no-results';
                noResultsDiv.innerHTML = `
                            <div class="no-results-icon">
                                <i class="fas fa-map-marked-alt"></i>
                            </div>
                            <div class="no-results-text">
                                No tours match your search criteria. Try adjusting your filters.
                            </div>
                        `;
                tourGrid.appendChild(noResultsDiv);
            }
        } else if (noResults) {
            noResults.remove();
        }
    });

    resetBtn.addEventListener('click', function () {
        // Reset all filters
        document.getElementById('destination-filter').value = '';
        document.getElementById('type-filter').value = '';
        priceSlider.value = 200;
        priceValue.textContent = '$200';

        // Uncheck all checkboxes
        document.querySelectorAll('input[type="checkbox"]').forEach(checkbox => {
            checkbox.checked = false;
        });

        // Show all tours
        tourCards.forEach(card => {
            card.style.display = 'block';
        });

        // Update results count
        document.querySelector('.results-count').textContent = `Showing 6 of 18 tours`;

        // Remove no results message if present
        const noResults = document.querySelector('.no-results');
        if (noResults) {
            noResults.remove();
        }
    });

    // Wishlist functionality
    const wishlistButtons = document.querySelectorAll('.wishlist-btn');

    wishlistButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            e.preventDefault();
            this.classList.toggle('active');

            // Here you would typically make an AJAX call to save to wishlist
            const tourCard = this.closest('.tour-card');
            const tourName = tourCard.querySelector('.tour-name').textContent;

            if (this.classList.contains('active')) {
                console.log(`Added ${tourName} to wishlist`);
            } else {
                console.log(`Removed ${tourName} from wishlist`);
            }
        });
    });
});

