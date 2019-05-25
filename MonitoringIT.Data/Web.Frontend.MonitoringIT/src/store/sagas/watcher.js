import { takeLatest } from "redux-saga/effects";
import types from "store/types";

import { getAllGithubProfiles, getFavoriteGithubProfiles } from "./githubProfiles";
import { getFavoriteLinkedinProfiles, getAllLinkedinProfiles } from "./linkedinProfiles";
import { getFavoriteCompanies, getCompanies } from "./companies";
import { getJobs } from "./jobs";
import { getProfileData } from "./profile";

export default function* root() {
	yield takeLatest(types.REQUESTED_JOBS, getJobs);
	yield takeLatest(types.REQUESTED_COMPANIES, getCompanies);
	yield takeLatest(types.REQUESTED_ALL_GITHUB_PROFILES, getAllGithubProfiles);
	yield takeLatest(types.REQUESTED_ALL_LINKEDIN_PROFILES, getAllLinkedinProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_GITHUB_PROFILES, getFavoriteGithubProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_LINKEDIN_PROFILES, getFavoriteLinkedinProfiles);
	yield takeLatest(types.REQUESTED_FAVORITE_COMPANIES, getFavoriteCompanies);
	yield takeLatest(types.REQUESTED_PROFILE, getProfileData);
}