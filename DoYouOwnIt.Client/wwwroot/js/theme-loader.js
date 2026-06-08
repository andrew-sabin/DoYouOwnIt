// EasyAppDev.Blazor.AutoComplete Theme Loader
// This script injects essential styles and loads theme CSS dynamically

(function() {
    // Inject minimal base CSS styles to ensure component displays correctly
    const baseStyles = `
    :root {
        --ebd-ac-primary: #3b82f6;
        --ebd-ac-bg: #ffffff;
        --ebd-ac-text: #1f2937;
        --ebd-ac-border: #d1d5db;
        --ebd-ac-border-focus: var(--ebd-ac-primary);
        --ebd-ac-hover: #f3f4f6;
        --ebd-ac-selected: #eff6ff;
        --ebd-ac-selected-text: var(--ebd-ac-primary);
        --ebd-ac-input-padding: 8px 32px 8px 12px;
        --ebd-ac-item-padding: 8px 12px;
        --ebd-ac-border-radius: 6px;
        --ebd-ac-dropdown-gap: 4px;
        --ebd-ac-dropdown-max-height: 300px;
        --ebd-ac-font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        --ebd-ac-font-size: 14px;
    }
    .ebd-ac-container { position: relative; width: 100%; font-family: var(--ebd-ac-font-family); }
    .ebd-ac-input-wrapper { position: relative; display: flex; align-items: center; }
    .ebd-ac-input { width: 100%; padding: var(--ebd-ac-input-padding); border: 1px solid var(--ebd-ac-border); border-radius: var(--ebd-ac-border-radius); background-color: var(--ebd-ac-bg); color: var(--ebd-ac-text); font-size: var(--ebd-ac-font-size); }
    .ebd-ac-input:focus { border-color: var(--ebd-ac-border-focus); box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1); outline: none; }
    .ebd-ac-dropdown { position: absolute; top: calc(100% + var(--ebd-ac-dropdown-gap)); left: 0; right: 0; z-index: 1000; background-color: var(--ebd-ac-bg); border: 1px solid var(--ebd-ac-border); border-radius: var(--ebd-ac-border-radius); box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1); max-height: var(--ebd-ac-dropdown-max-height); overflow-y: auto; }
    .ebd-ac-list { list-style: none; margin: 0; padding: 4px 0; }
    .ebd-ac-item { padding: var(--ebd-ac-item-padding); cursor: pointer; color: var(--ebd-ac-text); transition: background-color 0.15s; }
    .ebd-ac-item:hover { background-color: var(--ebd-ac-hover); }
    .ebd-ac-item.selected { background-color: var(--ebd-ac-selected); color: var(--ebd-ac-selected-text); font-weight: 500; }
    `;

    if (!document.querySelector('style[data-ebd-ac]')) {
        const styleEl = document.createElement('style');
        styleEl.setAttribute('data-ebd-ac', 'true');
        styleEl.textContent = baseStyles;
        document.head.appendChild(styleEl);
    }

    window.easyAppDevThemeLoader = {
        loadTheme: function(themePreset, primaryColor) {
            const themeName = (themePreset || 'modern').toLowerCase();
            const cssFile = `css/autocomplete.${themeName}.css`;

            // Check if theme CSS is already loaded
            if (document.querySelector(`link[href*="autocomplete.${themeName}"]`)) {
                return;
            }

            // Load theme CSS
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.href = cssFile;
            document.head.appendChild(link);

            if (primaryColor) {
                const style = document.createElement('style');
                style.textContent = `:root { --ebd-ac-primary: ${primaryColor}; }`;
                document.head.appendChild(style);
            }
        }
    };

    // Auto-load theme from script attribute
    const scriptTag = document.currentScript;
    if (scriptTag && scriptTag.dataset.theme) {
        window.easyAppDevThemeLoader.loadTheme(scriptTag.dataset.theme, scriptTag.dataset.primaryColor);
    }
})();
