import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home-2/home.component";
import { PersonsComponent } from "./components/persons-2/persons.component";

export const routes: Routes = [
    { path: "home-2", component: HomeComponent },
    { path: "persons", component: PersonsComponent },
    { path: "", redirectTo: "home-2", pathMatch: "full" }
]