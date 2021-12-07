import { HomeComponent } from "./components/home-2/home.component";
import { PersonsComponent } from "./components/persons-2/persons.component";
export const routes = [
    { path: "home", component: HomeComponent },
    { path: "persons", component: PersonsComponent },
    { path: "", redirectTo: "home", pathMatch: "full" }
];
