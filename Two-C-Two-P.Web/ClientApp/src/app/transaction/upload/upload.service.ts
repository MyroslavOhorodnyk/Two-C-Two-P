import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class UploadService {
  apiServiceBaseUrl: string;

  constructor(
    @Inject("BASE_URL") baseUrl: string,
    private http: HttpClient
  ) {
    this.apiServiceBaseUrl = baseUrl + "api/transactions/upload";
  }

  upload(formData: FormData) {
    return this.http.post<UploadResult>(this.apiServiceBaseUrl, formData);
  }
}

export interface UploadResult {
  info: string[];
  errors: string[];
}
