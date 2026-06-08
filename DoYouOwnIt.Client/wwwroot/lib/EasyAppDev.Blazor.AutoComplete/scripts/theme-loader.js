// EasyAppDev.Blazor.AutoComplete Theme Loader
// This script loads theme CSS files dynamically based on the ThemePreset parameter

(function() {
    window.easyAppDevThemeLoader = {
        loadTheme: function(themePreset, primaryColor) {
            // Map theme presets to CSS file names
            const themeMap = {
                'material': 'autocomplete.material.css',
                'fluent': 'autocomplete.fluent.css',
                'modern': 'autocomplete.modern.css',
                'bootstrap': 'autocomplete.bootstrap.css'
            };

            const themeName = (themePreset || 'modern').toLowerCase();
            const cssFile = themeMap[themeName] || 'autocomplete.modern.css';
            const basePath = '/_content/EasyAppDev.Blazor.AutoComplete/styles/';

            // Check if theme CSS is already loaded
            const existingLink = document.querySelector(`link[href*="${cssFile}"]`);
            if (existingLink) {
                return; // Already loaded
            }

            // Create and inject theme CSS link
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.href = basePath + cssFile;
            link.type = 'text/css';
            document.head.appendChild(link);

            // Apply custom primary color if provided
            if (primaryColor) {
                const style = document.createElement('style');
                style.textContent = `:root { --ac-primary-color: ${primaryColor}; }`;
                document.head.appendChild(style);
            }
        }
    };

    // Auto-load theme when script loads (if data attribute is present)
    const scriptTag = document.currentScript;
    if (scriptTag && scriptTag.dataset.theme) {
        window.easyAppDevThemeLoader.loadTheme(scriptTag.dataset.theme, scriptTag.dataset.primaryColor);
    }
})();
