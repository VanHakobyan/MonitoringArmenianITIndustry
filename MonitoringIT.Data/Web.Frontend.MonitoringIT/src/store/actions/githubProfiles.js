import types from "store/types";

// All
export const requestAllGithubProfiles = (currentPage, count) =>
	({ type: types.REQUESTED_ALL_GITHUB_PROFILES, currentPage, count });
export const succeededAllGithubProfiles = profiles => ({ type: types.SUCCEEDED_ALL_GITHUB_PROFILES, profiles });
export const failedAllGithubProfiles = error => ({ type: types.FAILED_ALL_GITHUB_PROFILES, error });

// presentation data
export const requestFavoriteGithubProfiles = count => ({ type: types.REQUESTED_FAVORITE_GITHUB_PROFILES, count });
export const succeededFavoriteGithubProfiles = profiles => ({ type: types.SUCCEEDED_FAVORITE_GITHUB_PROFILES, profiles });
export const failedFavoriteGithubProfiles = error => ({ type: types.FAILED_FAVORITE_GITHUB_PROFILES, error });
