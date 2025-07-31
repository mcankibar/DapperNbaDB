// NBA Team Selection JavaScript

let selectedTeamData = {
    id: null,
    name: '',
    abbreviation: '',
    logoPath: ''
};

// Team selection function
function selectTeam(teamId, teamName, abbreviation) {
    selectedTeamData = {
        id: teamId,
        name: teamName,
        abbreviation: abbreviation,
        logoPath: `/logos/${abbreviation.toLowerCase()}.png`
    };

    // Update modal content
    document.getElementById('selectedTeamName').textContent = teamName;
    document.getElementById('selectedTeamFullName').textContent = teamName;
    document.getElementById('selectedTeamAbbreviation').textContent = abbreviation;
    document.getElementById('selectedTeamLogo').src = selectedTeamData.logoPath;
    document.getElementById('selectedTeamLogo').alt = `${teamName} Logo`;

    // Show modal
    const modal = new bootstrap.Modal(document.getElementById('teamSelectionModal'));
    modal.show();

    // Add basketball animation to the card
    animateTeamSelection(teamId);
}

// Animate team selection
function animateTeamSelection(teamId) {
    const cards = document.querySelectorAll('.team-selection-card');
    cards.forEach(card => {
        const cardTeamId = card.getAttribute('onclick').match(/selectTeam\((\d+)/)?.[1];
        if (cardTeamId === teamId.toString()) {
            card.classList.add('team-selected');
            
            // Create basketball animation
            createBasketballAnimation(card);
        } else {
            card.style.opacity = '0.5';
        }
    });

    // Reset after animation
    setTimeout(() => {
        cards.forEach(card => {
            card.classList.remove('team-selected');
            card.style.opacity = '1';
        });
    }, 2000);
}

// Create basketball animation
function createBasketballAnimation(card) {
    const basketball = document.createElement('div');
    basketball.innerHTML = '<i class="fas fa-basketball-ball"></i>';
    basketball.style.cssText = `
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-size: 30px;
        color: #ff6b35;
        animation: basketballBounce 1s ease-in-out;
        z-index: 1000;
        pointer-events: none;
    `;

    card.style.position = 'relative';
    card.appendChild(basketball);

    setTimeout(() => {
        if (basketball.parentNode) {
            basketball.parentNode.removeChild(basketball);
        }
    }, 1000);
}

// Navigation functions
function viewTeamDetails() {
    if (selectedTeamData.id) {
        window.location.href = `/Teams/Details/${selectedTeamData.id}`;
    }
}

function viewTeamStats() {
    if (selectedTeamData.id) {
        // You can implement this later
        showToast('Team Statistics', 'Statistics page will be implemented soon!', 'info');
    }
}

function viewTeamPlayers() {
    if (selectedTeamData.id) {
        // You can implement this later
        showToast('Team Roster', 'Player roster page will be implemented soon!', 'info');
    }
}

function viewTeamGames() {
    if (selectedTeamData.id) {
        // You can implement this later
        showToast('Team Games', 'Games & schedule page will be implemented soon!', 'info');
    }
}

// Test database connection
async function testConnection() {
    const testBtn = event.target;
    const originalText = testBtn.innerHTML;
    
    testBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Testing...';
    testBtn.disabled = true;

    try {
        const response = await fetch('/Teams/TestConnection', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        const result = await response.json();
        
        if (result.success) {
            showToast('Connection Test', 'Database connection successful!', 'success');
        } else {
            showToast('Connection Test', `Connection failed: ${result.message}`, 'error');
        }
    } catch (error) {
        showToast('Connection Test', `Error: ${error.message}`, 'error');
    } finally {
        testBtn.innerHTML = originalText;
        testBtn.disabled = false;
    }
}

// Toast notification system
function showToast(title, message, type = 'info') {
    const toastContainer = getOrCreateToastContainer();
    
    const toastId = 'toast_' + Date.now();
    const iconClass = getIconForType(type);
    const bgClass = getBgClassForType(type);
    
    const toastHtml = `
        <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header ${bgClass} text-white">
                <i class="${iconClass} me-2"></i>
                <strong class="me-auto">${title}</strong>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast"></button>
            </div>
            <div class="toast-body">
                ${message}
            </div>
        </div>
    `;
    
    toastContainer.insertAdjacentHTML('beforeend', toastHtml);
    
    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement, { delay: 5000 });
    toast.show();
    
    // Remove from DOM after hiding
    toastElement.addEventListener('hidden.bs.toast', () => {
        toastElement.remove();
    });
}

function getOrCreateToastContainer() {
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        container.style.zIndex = '1055';
        document.body.appendChild(container);
    }
    return container;
}

function getIconForType(type) {
    const icons = {
        'success': 'fas fa-check-circle',
        'error': 'fas fa-exclamation-circle',
        'warning': 'fas fa-exclamation-triangle',
        'info': 'fas fa-info-circle'
    };
    return icons[type] || icons['info'];
}

function getBgClassForType(type) {
    const bgClasses = {
        'success': 'bg-success',
        'error': 'bg-danger',
        'warning': 'bg-warning',
        'info': 'bg-info'
    };
    return bgClasses[type] || bgClasses['info'];
}

// Add CSS animations
const style = document.createElement('style');
style.textContent = `
    @keyframes basketballBounce {
        0% { transform: translate(-50%, -50%) scale(0) rotate(0deg); opacity: 0; }
        50% { transform: translate(-50%, -50%) scale(1.2) rotate(180deg); opacity: 1; }
        100% { transform: translate(-50%, -50%) scale(1) rotate(360deg); opacity: 0; }
    }
    
    .team-selected {
        border-color: var(--nba-orange) !important;
        box-shadow: 0 0 20px rgba(255, 107, 53, 0.5) !important;
        transform: scale(1.02) !important;
    }
    
    .toast-container {
        z-index: 1055 !important;
    }
`;
document.head.appendChild(style);

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    console.log('NBA Team Selection initialized');
    
    // Add hover effects to team cards
    const teamCards = document.querySelectorAll('.team-selection-card');
    teamCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.borderColor = 'var(--nba-blue)';
        });
        
        card.addEventListener('mouseleave', function() {
            if (!this.classList.contains('team-selected')) {
                this.style.borderColor = 'transparent';
            }
        });
    });
});
