import types from "store/types";

export const requestAllGithubProfiles = () => ({ type: types.REQUESTED_ALL_GITHUB_PROFILES });
export const succeededAllGithubProfiles = profiles => ({ type: types.SUCCEEDED_ALL_GITHUB_PROFILES, profiles });
export const failedAllGithubProfiles = error => ({ type: types.FAILED_ALL_GITHUB_PROFILES, error });
