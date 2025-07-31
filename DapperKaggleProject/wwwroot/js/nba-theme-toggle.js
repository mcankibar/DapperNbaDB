/* NBA Theme Toggle & Animation JavaScript */

// NBA Theme Toggle Functionality
document.addEventListener('DOMContentLoaded', function() {
    initNBAThemeToggle();
    addBasketballEffects();
});

function initNBAThemeToggle() {
    const toggleInput = document.getElementById('toggleInput');
    if (toggleInput) {
        toggleInput.addEventListener('change', function() {
            const body = document.body;
            body.classList.add('theme-transition');
            
            if (this.checked) {
                // Switch to Dapper theme (red)
                body.classList.remove('ef-core-theme');
                body.classList.add('dapper-theme');
                localStorage.setItem('nba-theme', 'dapper');
                
                // Update sidebar gradient for Dapper
                const sidebar = document.querySelector('.sidebar');
                if (sidebar) {
                    sidebar.style.background = 'linear-gradient(180deg, #C8102E 10%, #c71e37 100%)';
                }
                
                // Update active nav link
                const activeNavLinks = document.querySelectorAll('.sidebar .nav-link.active');
                activeNavLinks.forEach(link => {
                    link.style.background = 'linear-gradient(45deg, #C8102E, #FF6900)';
                });
                
                console.log('🏀 Switched to Dapper Theme (Red)');
            } else {
                // Switch to EF Core theme (blue)
                body.classList.remove('dapper-theme');
                body.classList.add('ef-core-theme');
                localStorage.setItem('nba-theme', 'efcore');
                
                // Update sidebar gradient for EF Core
                const sidebar = document.querySelector('.sidebar');
                if (sidebar) {
                    sidebar.style.background = 'linear-gradient(180deg, #1D428A 10%, #17408B 100%)';
                }
                
                // Update active nav link
                const activeNavLinks = document.querySelectorAll('.sidebar .nav-link.active');
                activeNavLinks.forEach(link => {
                    link.style.background = 'linear-gradient(45deg, #C8102E, #FF6900)';
                });
                
                console.log('🏀 Switched to EF Core Theme (Blue)');
            }
            
            // Remove transition class after animation
            setTimeout(() => {
                body.classList.remove('theme-transition');
            }, 500);
        });
        
        // Load saved theme
        const savedTheme = localStorage.getItem('nba-theme');
        if (savedTheme === 'dapper') {
            toggleInput.checked = true;
            document.body.classList.add('dapper-theme');
            const sidebar = document.querySelector('.sidebar');
            if (sidebar) {
                sidebar.style.background = 'linear-gradient(180deg, #C8102E 10%, #c71e37 100%)';
            }
        } else {
            document.body.classList.add('ef-core-theme');
        }
    }
}

// Basketball bounce effect for nav items
function addBasketballEffects() {
    const navItems = document.querySelectorAll('.sidebar .nav-item');
    navItems.forEach(item => {
        item.addEventListener('mouseenter', function() {
            const basketball = this.querySelector('.basketball-indicator i');
            if (basketball) {
                basketball.style.animation = 'bounce 0.6s ease-in-out';
            }
            
            // Add court shine effect
            const navLink = this.querySelector('.nav-link');
            if (navLink && !navLink.classList.contains('active')) {
                navLink.style.background = 'linear-gradient(90deg, rgba(255,255,255,0.1), rgba(253,185,39,0.2), rgba(255,255,255,0.1))';
            }
        });
        
        item.addEventListener('mouseleave', function() {
            const basketball = this.querySelector('.basketball-indicator i');
            if (basketball) {
                setTimeout(() => {
                    basketball.style.animation = '';
                }, 600);
            }
            
            // Remove court shine effect
            const navLink = this.querySelector('.nav-link');
            if (navLink && !navLink.classList.contains('active')) {
                navLink.style.background = '';
            }
        });
        
        // Add click animation
        item.addEventListener('click', function() {
            const icon = this.querySelector('.nav-link i');
            if (icon) {
                icon.style.transform = 'scale(1.3) rotate(15deg)';
                setTimeout(() => {
                    icon.style.transform = '';
                }, 300);
            }
        });
    });
}

// Add court shimmer effect to sidebar brand
function addCourtShimmer() {
    const sidebarBrand = document.querySelector('.sidebar-brand-icon');
    if (sidebarBrand) {
        setInterval(() => {
            sidebarBrand.style.filter = 'drop-shadow(0 0 15px rgba(253, 185, 39, 0.8))';
            setTimeout(() => {
                sidebarBrand.style.filter = 'drop-shadow(0 0 10px rgba(253, 185, 39, 0.5))';
            }, 500);
        }, 2000);
    }
}

// Initialize court effects
document.addEventListener('DOMContentLoaded', function() {
    addCourtShimmer();
});
