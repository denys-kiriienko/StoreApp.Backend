import { logout } from './auth.js';

export async function fetchWithCredentials(url, options) {
    options = options || {};
    options.credentials = 'include';

    let response = await fetch(url, options);

    if (response.status === 401) {
        try {
            const refreshResponse = await fetch("https://localhost:7056/api/auth/refresh-token", {
                method: "POST",
                credentials: "include"
            });

            if (!refreshResponse.ok) {
                throw new Error("Unable to refresh token");
            }

            response = await fetch(url, options);
        } catch (error) {
            console.error("Auto-refresh failed:", error);
            await logout();
            throw new Error("Unauthorized and refresh failed");
        }
    }

    if (!response.ok) {
        throw new Error('Fetch failed: ' + response.status);
    }

    const contentType = response.headers.get('content-type');

    if (contentType && contentType.includes('application/json')) {
        return await response.json();
    } else {
        return await response.text();
    }
}
