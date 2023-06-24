import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [{ 
  path: "drivers-management",

  loadChildren: () =>
    import(
      "./drivers-management/drivers-management.module"
    ).then((m) => m.DriversManagementModule),
},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
