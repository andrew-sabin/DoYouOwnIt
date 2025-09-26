
export function setupLightboxKeyboard(dotNetHelper) {
    document.addEventListener('keydown', (e) => {
        // Only handle these keys when lightbox is open
        if (['Escape', 'ArrowLeft', 'ArrowRight'].includes(e.key)) {
            dotNetHelper.invokeMethodAsync('HandleKeyDown', e.key);
        }
    });
}