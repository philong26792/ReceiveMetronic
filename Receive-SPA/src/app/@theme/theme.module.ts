import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarMenuComponent } from './components/sidebar-menu/sidebar-menu.component';
import { FooterComponent } from './components/footer/footer.component';



const COMPONENTS = [
    HeaderComponent,
    SidebarMenuComponent,
    FooterComponent,
    LayoutComponent
];
@NgModule({
    imports: [CommonModule],
    exports: [ ...COMPONENTS],
    declarations: [...COMPONENTS],
})
export class ThemeModule { }
