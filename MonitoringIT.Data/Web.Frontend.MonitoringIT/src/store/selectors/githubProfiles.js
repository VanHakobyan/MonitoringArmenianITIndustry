export const allProfilesLoadingSelector = state => state.githubProfiles.allProfilesLoading;
export const allProfilesSuccessSelector = state => state.githubProfiles.allProfilesSuccess;
export const allProfilesFailedSelector = state => state.githubProfiles.allProfilesFailed;
//
export const favoriteGithubProfilesLoadingSelector = state => state.githubProfiles.favoriteGithubProfilesLoading;
export const favoriteGithubProfilesSuccessSelector = state => state.githubProfiles.favoriteGithubProfilesSuccess;
export const favoriteGithubProfilesFailedSelector = state => state.githubProfiles.favoriteGithubProfilesFailed;
//
export const currentGithubPageSelector = state => state.githubProfiles.currentGithubPage;