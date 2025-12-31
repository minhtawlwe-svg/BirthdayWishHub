// ===== SIGNALR =====
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.on("ReceiveUpdate", () => loadMessages());
connection.start().catch(err => console.error(err.toString()));

// ===== LOAD MESSAGES =====
async function loadMessages() {
    const res = await fetch('/api/messages');
    const data = await res.json();
    const list = document.getElementById('messageList');
    list.innerHTML = '';

    data.forEach(m => {
        let mediaHtml = '';
        if (m.mediaPath) {
            if (m.mediaPath.endsWith('.mp4') || m.mediaPath.endsWith('.webm')) {
                mediaHtml = `<video src="${m.mediaPath}" controls class="w-100 rounded mt-2" style="max-height:300px;object-fit:cover;"></video>`;
            } else {
                mediaHtml = `<img src="${m.mediaPath}" class="img-fluid rounded mt-2" alt="media" />`;
            }
        }

        const reacted = hasReacted(m.id);

        list.innerHTML += `
        <div class="section-card message-card mb-3 p-3 shadow-sm rounded">
            <h5 class="text-danger">${escapeHtml(m.sender)}</h5>
            <p class="fs-5">${escapeHtml(m.text)}</p>
            ${mediaHtml}

            <!-- Reactions -->
            <div class="mt-3 d-flex gap-2 flex-wrap align-items-center">
                <button class="btn btn-outline-danger btn-sm" onclick="react(${m.id}, 'love')" ${reacted ? 'disabled' : ''}>❤️ <span>${m.love}</span></button>
                <button class="btn btn-outline-warning btn-sm" onclick="react(${m.id}, 'laugh')" ${reacted ? 'disabled' : ''}>😂 <span>${m.laugh}</span></button>
                <button class="btn btn-outline-info btn-sm" onclick="react(${m.id}, 'wow')" ${reacted ? 'disabled' : ''}>😮 <span>${m.wow}</span></button>
                <button class="btn btn-outline-success btn-sm" onclick="react(${m.id}, 'clap')" ${reacted ? 'disabled' : ''}>👏 <span>${m.clap}</span></button>
                <button class="btn btn-outline-primary btn-sm" onclick="react(${m.id}, 'celebrate')" ${reacted ? 'disabled' : ''}>🎉 <span>${m.celebrate}</span></button>
            </div>

            ${reacted ? '<small class="text-muted mt-1 d-block">You already reacted ❤️</small>' : ''}
        </div>`;
    });
}

// ===== POST MESSAGE =====
async function postMessage() {
    const sender = document.getElementById('sender').value.trim();
    const text = document.getElementById('text').value.trim();
    const media = document.getElementById('media').files[0];

    if (!sender || !text) { 
        alert('Enter name and message'); 
        return; 
    }

    const form = new FormData();
    form.append('sender', sender);
    form.append('text', text);
    if (media) form.append('media', media);

    await fetch('/api/messages/upload', { method: 'POST', body: form });

    document.getElementById('text').value = '';
    document.getElementById('media').value = '';
}

// ===== REACTIONS =====
function hasReacted(id) { 
    return localStorage.getItem('reacted_' + id) !== null; 
}

async function react(id, type) {
    if (hasReacted(id)) { 
        alert('You already reacted ❤️'); 
        return; 
    }

    await fetch(`/api/messages/${id}/react/${type}`, { method: 'POST' });
    localStorage.setItem('reacted_' + id, 'true');
}

// ===== XSS PROTECTION =====
function escapeHtml(text) {
    const div = document.createElement('div');
    div.innerText = text;
    return div.innerHTML;
}
