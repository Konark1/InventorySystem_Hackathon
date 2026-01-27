import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="landing-page">
      <!-- Clean Navigation -->
      <nav class="navbar">
        <div class="nav-container">
          <div class="nav-left">
            <div class="logo">
              <div class="logo-icon">
                <svg viewBox="0 0 24 24" fill="currentColor">
                  <rect x="3" y="3" width="7" height="7" rx="2"/>
                  <rect x="14" y="3" width="7" height="7" rx="2"/>
                  <rect x="14" y="14" width="7" height="7" rx="2"/>
                  <rect x="3" y="14" width="7" height="7" rx="2"/>
                </svg>
              </div>
              <span class="logo-text">InventoryPro</span>
            </div>
            <div class="nav-menu">
              <a href="#features" class="nav-link">Features</a>
              <a href="#pricing" class="nav-link">Pricing</a>
              <a href="#integrations" class="nav-link">Integrations</a>
              <a href="#resources" class="nav-link">Resources</a>
            </div>
          </div>
          <div class="nav-right">
            <button class="btn-signin" (click)="navigateToLogin()">Sign In</button>
            <button class="btn-getstarted" (click)="navigateToRegister()">Get Started</button>
          </div>
        </div>
      </nav>

      <!-- Hero Section with Mockup -->
      <section class="hero-section">
        <div class="hero-container">
          <div class="hero-content">
            <div class="hero-badge">Smart Inventory Management</div>
            <h1 class="hero-title">
              Streamline Your<br/>
              <span class="gradient-text">Inventory Operations</span>
            </h1>
            <p class="hero-description">
              Complete control over your stock with real-time tracking, automated workflows, 
              and intelligent forecasting. Manage warehouses, products, and operations from one platform.
            </p>
            <div class="hero-cta">
              <button class="btn-start-trial" (click)="navigateToRegister()">
                Start Free Trial
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor">
                  <path d="M5 12h14M12 5l7 7-7 7"/>
                </svg>
              </button>
              <button class="btn-watch-demo">
                <svg viewBox="0 0 24 24" fill="currentColor">
                  <path d="M8 5v14l11-7z"/>
                </svg>
                Watch Demo
              </button>
            </div>
            <div class="hero-stats">
              <div class="stat-item">
                <span class="stat-number">12M+</span>
                <span class="stat-label">Users worldwide</span>
              </div>
              <div class="stat-item">
                <span class="stat-number">7M+</span>
                <span class="stat-label">Companies</span>
              </div>
              <div class="stat-item">
                <span class="stat-number">4.5★</span>
                <span class="stat-label">Rating</span>
              </div>
            </div>
          </div>

          <!-- Dashboard Mockup (like Figma design) -->
          <div class="hero-mockup">
            <div class="mockup-container">
              <div class="mockup-header">
                <div class="mockup-controls">
                  <span class="control-dot red"></span>
                  <span class="control-dot yellow"></span>
                  <span class="control-dot green"></span>
                </div>
                <div class="mockup-search">
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor">
                    <circle cx="11" cy="11" r="8"/>
                    <path d="m21 21-4.35-4.35"/>
                  </svg>
                  <span>Search products...</span>
                </div>
                <div class="mockup-user">
                  <div class="user-avatar">JD</div>
                  <span>John Doe</span>
                </div>
              </div>
              <div class="mockup-content">
                <div class="table-header">
                  <h3>Products</h3>
                  <button class="btn-add-product">+ Add Product</button>
                </div>
                <table class="mockup-table">
                  <thead>
                    <tr>
                      <th>Product</th>
                      <th>SKU</th>
                      <th>Category</th>
                      <th>Stock</th>
                      <th>Price</th>
                      <th>Status</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>
                        <div class="product-cell">
                          <div class="product-name">Wireless Keyboard</div>
                          <div class="product-id">PRD-001</div>
                        </div>
                      </td>
                      <td>WK-2024-001</td>
                      <td>Electronics</td>
                      <td>245</td>
                      <td>$49.99</td>
                      <td><span class="badge in-stock">In Stock</span></td>
                    </tr>
                    <tr>
                      <td>
                        <div class="product-cell">
                          <div class="product-name">USB-C Cable</div>
                          <div class="product-id">PRD-002</div>
                        </div>
                      </td>
                      <td>CB-2024-002</td>
                      <td>Accessories</td>
                      <td>89</td>
                      <td>$12.99</td>
                      <td><span class="badge in-stock">In Stock</span></td>
                    </tr>
                    <tr>
                      <td>
                        <div class="product-cell">
                          <div class="product-name">Laptop Stand</div>
                          <div class="product-id">PRD-003</div>
                        </div>
                      </td>
                      <td>LS-2024-003</td>
                      <td>Furniture</td>
                      <td>15</td>
                      <td>$79.99</td>
                      <td><span class="badge low-stock">Low Stock</span></td>
                    </tr>
                    <tr>
                      <td>
                        <div class="product-cell">
                          <div class="product-name">Wireless Mouse</div>
                          <div class="product-id">PRD-004</div>
                        </div>
                      </td>
                      <td>WM-2024-004</td>
                      <td>Electronics</td>
                      <td>0</td>
                      <td>$29.99</td>
                      <td><span class="badge out-of-stock">Out of Stock</span></td>
                    </tr>
                  </tbody>
                </table>
                <div class="table-footer">
                  <span>Showing 1-4 of 2,847 products</span>
                  <div class="pagination">
                    <button>Previous</button>
                    <button class="active">1</button>
                    <button>Next</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- Integrations Section -->
      <section id="integrations" class="integrations-section">
        <div class="section-container">
          <div class="section-header-centered">
            <h2>Integrates with your favorite tools</h2>
            <p>Connect seamlessly with the apps and services you already use</p>
          </div>
          <div class="integrations-grid">
            <div class="integration-card">
              <div class="integration-icon shopify">🛍️</div>
              <span>Shopify</span>
            </div>
            <div class="integration-card">
              <div class="integration-icon amazon">📦</div>
              <span>Amazon</span>
            </div>
            <div class="integration-card">
              <div class="integration-icon woocommerce">🛒</div>
              <span>WooCommerce</span>
            </div>
            <div class="integration-card">
              <div class="integration-icon quickbooks">📊</div>
              <span>QuickBooks</span>
            </div>
            <div class="integration-card">
              <div class="integration-icon salesforce">☁️</div>
              <span>Salesforce</span>
            </div>
            <div class="integration-card">
              <div class="integration-icon stripe">💳</div>
              <span>Stripe</span>
            </div>
          </div>
        </div>
      </section>

      <!-- Features Section -->
      <section id="features" class="features-section">
        <div class="section-container">
          <div class="section-header-centered">
            <h2>Everything you need to manage inventory</h2>
            <p>Powerful features designed for modern businesses</p>
          </div>
          
          <div class="features-grid-modern">
            <div class="feature-card-modern">
              <div class="feature-icon-modern blue">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M12 2v20M17 5H9.5a3.5 3.5 0 000 7h5a3.5 3.5 0 010 7H6"/>
                </svg>
              </div>
              <h3>Real-Time Tracking</h3>
              <p>Monitor inventory levels across all locations with automatic updates and live synchronization</p>
            </div>

            <div class="feature-card-modern">
              <div class="feature-icon-modern purple">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M10.29 3.86L1.82 18a2 2 0 001.71 3h16.94a2 2 0 001.71-3L13.71 3.86a2 2 0 00-3.42 0z"/>
                  <line x1="12" y1="9" x2="12" y2="13"/>
                  <line x1="12" y1="17" x2="12.01" y2="17"/>
                </svg>
              </div>
              <h3>Smart Alerts</h3>
              <p>Get instant notifications when stock runs low, expires, or needs attention</p>
            </div>

            <div class="feature-card-modern">
              <div class="feature-icon-modern green">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <line x1="12" y1="20" x2="12" y2="10"/>
                  <line x1="18" y1="20" x2="18" y2="4"/>
                  <line x1="6" y1="20" x2="6" y2="16"/>
                </svg>
              </div>
              <h3>Advanced Analytics</h3>
              <p>Make data-driven decisions with comprehensive reports and actionable insights</p>
            </div>

            <div class="feature-card-modern">
              <div class="feature-icon-modern orange">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <rect x="3" y="11" width="18" height="11" rx="2" ry="2"/>
                  <path d="M7 11V7a5 5 0 0110 0v4"/>
                </svg>
              </div>
              <h3>Enterprise Security</h3>
              <p>Bank-level encryption and role-based access control to protect your data</p>
            </div>

            <div class="feature-card-modern">
              <div class="feature-icon-modern red">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/>
                  <circle cx="9" cy="7" r="4"/>
                  <path d="M23 21v-2a4 4 0 00-3-3.87"/>
                  <path d="M16 3.13a4 4 0 010 7.75"/>
                </svg>
              </div>
              <h3>Team Collaboration</h3>
              <p>Work together seamlessly with customizable permissions and user roles</p>
            </div>

            <div class="feature-card-modern">
              <div class="feature-icon-modern teal">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M21 16V8a2 2 0 00-1-1.73l-7-4a2 2 0 00-2 0l-7 4A2 2 0 003 8v8a2 2 0 001 1.73l7 4a2 2 0 002 0l7-4A2 2 0 0021 16z"/>
                  <polyline points="3.27 6.96 12 12.01 20.73 6.96"/>
                  <line x1="12" y1="22.08" x2="12" y2="12"/>
                </svg>
              </div>
              <h3>Multi-Warehouse</h3>
              <p>Manage multiple locations from a single dashboard with unified reporting</p>
            </div>
          </div>
        </div>
      </section>

      <!-- CTA Section -->
      <section class="cta-section-modern">
        <div class="cta-container">
          <div class="cta-content">
            <h2>Ready to streamline your inventory?</h2>
            <p>Join thousands of businesses using InventoryPro to manage their operations</p>
            <div class="cta-buttons-modern">
              <button class="btn-cta-primary-modern" (click)="navigateToRegister()">Start Free Trial</button>
              <button class="btn-cta-secondary-modern" (click)="navigateToLogin()">Sign In</button>
            </div>
            <p class="cta-disclaimer">No credit card required • 14-day free trial • Cancel anytime</p>
          </div>
        </div>
      </section>

      <!-- Footer -->
      <footer class="footer-modern">
        <div class="footer-container">
          <div class="footer-grid">
            <div class="footer-brand">
              <div class="logo">
                <div class="logo-icon">
                  <svg viewBox="0 0 24 24" fill="currentColor">
                    <rect x="3" y="3" width="7" height="7" rx="2"/>
                    <rect x="14" y="3" width="7" height="7" rx="2"/>
                    <rect x="14" y="14" width="7" height="7" rx="2"/>
                    <rect x="3" y="14" width="7" height="7" rx="2"/>
                  </svg>
                </div>
                <span class="logo-text">InventoryPro</span>
              </div>
              <p class="footer-description">Modern inventory management for modern businesses. Streamline operations and grow with confidence.</p>
            </div>
            <div class="footer-links-group">
              <h4>Product</h4>
              <a href="#">Features</a>
              <a href="#">Pricing</a>
              <a href="#">Integrations</a>
            </div>
            <div class="footer-links-group">
              <h4>Company</h4>
              <a href="#">About Us</a>
              <a href="#">Careers</a>
              <a href="#">Blog</a>
            </div>
            <div class="footer-links-group">
              <h4>Resources</h4>
              <a href="#">Documentation</a>
              <a href="#">Support</a>
              <a href="#">Status</a>
            </div>
          </div>
          <div class="footer-bottom">
            <p>&copy; 2026 InventoryPro. All rights reserved.</p>
          </div>
        </div>
      </footer>
    </div>
  `,
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  constructor(private router: Router) {}

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}
