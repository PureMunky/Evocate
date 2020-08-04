import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { LoggingComponent } from "./components/logging/logging.component";
import { LanguageAdminComponent } from "./components/languageAdmin/languageAdmin.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { TopicComponent } from "./components/topic/topic.component";
import { MentorshipComponent } from "./components/mentorship/mentorship.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoggingComponent,
    LanguageAdminComponent,
    ProfileComponent,
    TopicComponent,
    MentorshipComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "logging", component: LoggingComponent },
      { path: "languageAdmin", component: LanguageAdminComponent },
      { path: "profile/:username", component: ProfileComponent },
      { path: "topic/create/:name", component: TopicComponent },
      { path: "topic/:topicId", component: TopicComponent },
      { path: "mentorship/:mentorshipId", component: MentorshipComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
