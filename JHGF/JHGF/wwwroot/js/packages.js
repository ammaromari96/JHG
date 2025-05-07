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
    const packageCards = document.querySelectorAll('.package-card');

    applyBtn.addEventListener('click', function () {
        const destinationFilter = document.getElementById('destination-filter').value;
        const typeFilter = document.getElementById('type-filter').value;
        const priceFilter = parseInt(priceSlider.value);
        const durationCheckboxes = document.querySelectorAll('input[name="duration"]:checked');
        const inclusionCheckboxes = document.querySelectorAll('input[name="inclusions"]:checked');

        let visiblePackages = 0;

        packageCards.forEach(card => {
            const cardDestination = card.getAttribute('data-destination').split(',');
            const cardType = card.getAttribute('data-type');
            const cardPrice = parseInt(card.getAttribute('data-price'));
            const cardDuration = card.getAttribute('data-duration');
            const cardInclusions = card.getAttribute('data-inclusions').split(',');

            // Check destination filter
            const destinationMatch = !destinationFilter ||
                (cardDestination.includes(destinationFilter)) ||
                (destinationFilter === 'multiple' && cardDestination.length > 1) ||
                (destinationFilter === '' && cardDestination);

            // Check type filter
            const typeMatch = !typeFilter ||
                (cardType === typeFilter) ||
                (typeFilter === '' && cardType);

            // Check price filter
            const priceMatch = cardPrice <= priceFilter;

            // Check duration filter
            let durationMatch = durationCheckboxes.length === 0;
            durationCheckboxes.forEach(checkbox => {
                const durationValue = checkbox.value;
                if (durationValue === '3' && (cardDuration === '3' || cardDuration === '4' || cardDuration === '5')) {
                    durationMatch = true;
                } else if (durationValue === '7' && cardDuration === '7') {
                    durationMatch = true;
                } else if (durationValue === '10' && parseInt(cardDuration) >= 10) {
                    durationMatch = true;
                }
            });

            // Check inclusions filter
            let inclusionsMatch = inclusionCheckboxes.length === 0;
            if (!inclusionsMatch) {
                const requiredInclusions = Array.from(inclusionCheckboxes).map(cb => cb.value);
                inclusionsMatch = requiredInclusions.every(inclusion =>
                    cardInclusions.includes(inclusion)
                );
            }

            // Show/hide card based on filters
            if (destinationMatch && typeMatch && priceMatch && durationMatch && inclusionsMatch) {
                card.style.display = 'block';
                visiblePackages++;
            } else {
                card.style.display = 'none';
            }
        });

        // Update results count
        document.querySelector('.results-count').textContent = `Showing ${visiblePackages} of 15 packages`;

        // Show no results message if no packages match
        const noResults = document.querySelector('.no-results');
        if (visiblePackages === 0) {
            if (!noResults) {
                const packageGrid = document.querySelector('.package-grid');
                const noResultsDiv = document.createElement('div');
                noResultsDiv.className = 'no-results';
                noResultsDiv.innerHTML = `
                            <div class="no-results-icon">
                                <i class="fas fa-suitcase"></i>
                            </div>
                            <div class="no-results-text">
                                No packages match your search criteria. Try adjusting your filters.
                            </div>
                        `;
                packageGrid.appendChild(noResultsDiv);
            }
        } else if (noResults) {
            noResults.remove();
        }
    });

    resetBtn.addEventListener('click', function () {
        // Reset all filters
        document.getElementById('destination-filter').value = '';
        document.getElementById('type-filter').value = '';
        priceSlider.value = 1000;
        priceValue.textContent = '$1000';

        // Uncheck all checkboxes
        document.querySelectorAll('input[type="checkbox"]').forEach(checkbox => {
            checkbox.checked = false;
        });

        // Show all packages
        packageCards.forEach(card => {
            card.style.display = 'block';
        });

        // Update results count
        document.querySelector('.results-count').textContent = `Showing 6 of 15 packages`;

        // Remove no results message if present
        const noResults = document.querySelector('.no-results');
        if (noResults) {
            noResults.remove();
        }
    });

    //// Wishlist functionality
    //const wishlistButtons = document.querySelectorAll('.wishlist-btn');

    //wishlistButtons.forEach(button => {
    //    button.addEventListener('click', function (e) {
    //        e.preventDefault();
    //        this.classList.toggle('active');

    //        // Here you would typically make an AJAX call to save to wishlist
    //        const packageCard = this.closest('.package-card');
    //        const packageName = packageCard.querySelector('.package-name').textContent;

    //        if (this.classList.contains('active')) {
    //            console.log(`Added ${packageName} to wishlist`);
    //        } else {
    //            console.log(`Removed ${packageName} from wishlist`);
    //        }
    //    });
    //});
});