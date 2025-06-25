export async function login(email, password) {
    const response = await fetch("https://localhost:7056/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
        credentials: "include"
    });

    if (!response.ok) throw new Error("Login failed");

    return await response.json();
}

export async function logout() {
    await fetch("https://localhost:7056/api/auth/logout", {
        method: "POST",
        credentials: "include"
    });
}

export async function refreshToken() {
    const response = await fetch("https://localhost:7056/api/auth/refresh-token", {
        method: "POST",
        credentials: "include"
    });

    if (!response.ok) throw new Error("Refresh failed");

    return await response.json();
}
