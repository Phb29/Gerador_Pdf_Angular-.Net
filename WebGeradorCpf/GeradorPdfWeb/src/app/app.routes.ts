import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from '../list/list.component';
import { NgModule } from '@angular/core';
import { HomeComponent } from '../home/home.component';

export const routes: Routes = [
{ path: '', component: HomeComponent },
{ path: 'listagem', component: ListComponent }

];
    @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
    })
    export class AppRoutingModule { }