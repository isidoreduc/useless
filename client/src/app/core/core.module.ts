import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';

@NgModule({
  declarations: [NavbarComponent, SectionHeaderComponent],
  imports: [CommonModule, RouterModule, BreadcrumbModule],
  exports: [NavbarComponent, SectionHeaderComponent],
})
export class CoreModule {}
