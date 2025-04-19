import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'; // ✅ NECESAR pentru routerLink

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [RouterModule], // ✅ Aici adăugăm RouterModule
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {}
