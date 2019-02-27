"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var home_component_1 = require("./home/home.component");
var login_component_1 = require("./login/login.component");
var auth_guard_1 = require("./Authorization/auth.guard");
var role_guard_1 = require("./Authorization/role.guard");
var all_leads_component_1 = require("./all-leads/all-leads.component");
exports.appRoutes = [
    { path: 'home', component: home_component_1.HomeComponent, canActivate: [auth_guard_1.AuthGuard, role_guard_1.RoleGuard] },
    {
        path: 'login', component: login_component_1.LoginComponent
    },
    {
        path: 'AllLeads', component: all_leads_component_1.AllLeadsComponent, canActivate: [auth_guard_1.AuthGuard],
        data: {
            expectedRole: 'admin'
        }
    },
    { path: '', redirectTo: '/login', pathMatch: 'full' }
];
//# sourceMappingURL=routes.js.map