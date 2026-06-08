export function attachImageClickHandlers(containerElement, dotNetHelper) {
    if (!containerElement) return;

    // Find all img elements inside the container
    const images = containerElement.querySelectorAll('img');

    images.forEach((img, index) => {
        // Store original src for later use
        const imageUrl = img.src;

        // Make cursor pointer to indicate clickability
        img.style.cursor = 'pointer';

        // Add click handler
        img.addEventListener('click', (e) => {
            e.stopPropagation();
            dotNetHelper.invokeMethodAsync('OnImageClicked', imageUrl);
        });

        // Optional: add a subtle hover effect
        img.addEventListener('mouseenter', () => {
            img.style.opacity = '0.8';
            img.style.transition = 'opacity 0.2s';
        });
        img.addEventListener('mouseleave', () => {
            img.style.opacity = '1';
        });
    });
}

export function cleanup(containerElement) {
    if (!containerElement) return;
    const images = containerElement.querySelectorAll('img');
    images.forEach(img => {
        // Remove event listeners (optional, but good practice)
        img.style.cursor = '';
        img.style.opacity = '';
        // Clone and replace to fully remove listeners (if needed)
        // const newImg = img.cloneNode(true);
        // img.parentNode.replaceChild(newImg, img);
    });
}