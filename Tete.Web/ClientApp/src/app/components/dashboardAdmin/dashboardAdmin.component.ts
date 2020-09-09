import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "dashboard-admin",
  templateUrl: "./dashboardAdmin.component.html"
})
export class DashboardAdminComponent {
  public stats: DashboardStatistics = {
    totalUsers: 0,
    activeUsers: 0,
    totalTopics: 0,
    activeTopics: 0,
    recentTopics: 0,
    totalMentorships: 0,
    waitingMentorships: 0,
    activeMentorships: 0,
    completedMentorships: 0,
    cancelledMentorships: 0
  };

  constructor(
    private apiService: ApiService,
  ) {
    this.load();
  }

  public load() {
    return this.apiService.get("V1/Logs/Dashboard")
      .then(data => {
        this.stats = data[0];
      });
  };
}

interface DashboardStatistics {
  totalUsers: number,
  activeUsers: number,
  totalTopics: number,
  activeTopics: number,
  recentTopics: number,
  totalMentorships: number,
  waitingMentorships: number,
  activeMentorships: number,
  completedMentorships: number,
  cancelledMentorships: number
}
