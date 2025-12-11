const API_URL = 'https://localhost:7247/api';

// Cambiar entre secciones
function showSection(section) {
    document.querySelectorAll('.section').forEach(s => s.classList.remove('active'));
    document.querySelectorAll('.tab').forEach(t => t.classList.remove('active'));
    
    document.getElementById(section).classList.add('active');
    event.target.classList.add('active');

    if (section === 'pacientes') loadPacientes();
    if (section === 'tecnicos') loadTecnicos();
    if (section === 'pruebas') loadPruebas();
    if (section === 'citas') {
        loadCitas();
        loadCitaSelects();
    }
}

function showAlert(elementId, message, type) {
    const alertDiv = document.getElementById(elementId);
    alertDiv.innerHTML = `<div class="alert alert-${type}">${message}</div>`;
    setTimeout(() => alertDiv.innerHTML = '', 5000);
}

// PACIENTES
async function loadPacientes() {
    try {
        const response = await fetch(`${API_URL}/Pacientes`);
        const pacientes = await response.json();
        
        const table = document.getElementById('pacientesTable');
        if (pacientes.length === 0) {
            table.innerHTML = '<div class="empty-state">No hay pacientes registrados</div>';
            return;
        }

        table.innerHTML = `
            <table>
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Apellido</th>
                        <th>Teléfono</th>
                        <th>Correo</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${pacientes.map(p => `
                        <tr>
                            <td>${p.nombre}</td>
                            <td>${p.apellido}</td>
                            <td>${p.telefono}</td>
                            <td>${p.correo}</td>
                            <td class="actions">
                                <button class="btn btn-warning" onclick="editPaciente('${p.id}')">✏️ Editar</button>
                                <button class="btn btn-danger" onclick="deletePaciente('${p.id}')">🗑️ Eliminar</button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        `;
    } catch (error) {
        showAlert('pacienteAlert', 'Error al cargar pacientes: ' + error.message, 'error');
    }
}

document.getElementById('pacienteForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const id = document.getElementById('pacienteId').value;
    const data = {
        nombre: document.getElementById('pacienteNombre').value,
        apellido: document.getElementById('pacienteApellido').value,
        telefono: document.getElementById('pacienteTelefono').value,
        correo: document.getElementById('pacienteCorreo').value
    };

    try {
        let response;
        if (id) {
            data.id = id;
            response = await fetch(`${API_URL}/Pacientes/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        } else {
            response = await fetch(`${API_URL}/Pacientes`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        }

        if (response.ok) {
            showAlert('pacienteAlert', id ? 'Paciente actualizado exitosamente' : 'Paciente creado exitosamente', 'success');
            clearPacienteForm();
            loadPacientes();
        } else {
            showAlert('pacienteAlert', 'Error al guardar paciente', 'error');
        }
    } catch (error) {
        showAlert('pacienteAlert', 'Error: ' + error.message, 'error');
    }
});

async function editPaciente(id) {
    try {
        const response = await fetch(`${API_URL}/Pacientes/${id}`);
        const paciente = await response.json();
        
        document.getElementById('pacienteId').value = paciente.id;
        document.getElementById('pacienteNombre').value = paciente.nombre;
        document.getElementById('pacienteApellido').value = paciente.apellido;
        document.getElementById('pacienteTelefono').value = paciente.telefono;
        document.getElementById('pacienteCorreo').value = paciente.correo;
        
        window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (error) {
        showAlert('pacienteAlert', 'Error al cargar paciente: ' + error.message, 'error');
    }
}

async function deletePaciente(id) {
    if (!confirm('¿Está seguro de eliminar este paciente?')) return;
    
    try {
        const response = await fetch(`${API_URL}/Pacientes/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            showAlert('pacienteAlert', 'Paciente eliminado exitosamente', 'success');
            loadPacientes();
        } else {
            showAlert('pacienteAlert', 'Error al eliminar paciente', 'error');
        }
    } catch (error) {
        showAlert('pacienteAlert', 'Error: ' + error.message, 'error');
    }
}

function clearPacienteForm() {
    document.getElementById('pacienteForm').reset();
    document.getElementById('pacienteId').value = '';
}

// TÉCNICOS
async function loadTecnicos() {
    try {
        const response = await fetch(`${API_URL}/Tecnicos`);
        const tecnicos = await response.json();
        
        const table = document.getElementById('tecnicosTable');
        if (tecnicos.length === 0) {
            table.innerHTML = '<div class="empty-state">No hay técnicos registrados</div>';
            return;
        }

        table.innerHTML = `
            <table>
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Especialidad</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${tecnicos.map(t => `
                        <tr>
                            <td>${t.nombre}</td>
                            <td>${t.especialidad}</td>
                            <td class="actions">
                                <button class="btn btn-warning" onclick="editTecnico('${t.id}')">✏️ Editar</button>
                                <button class="btn btn-danger" onclick="deleteTecnico('${t.id}')">🗑️ Eliminar</button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        `;
    } catch (error) {
        showAlert('tecnicoAlert', 'Error al cargar técnicos: ' + error.message, 'error');
    }
}

document.getElementById('tecnicoForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const id = document.getElementById('tecnicoId').value;
    const data = {
        nombre: document.getElementById('tecnicoNombre').value,
        especialidad: document.getElementById('tecnicoEspecialidad').value
    };

    try {
        let response;
        if (id) {
            data.id = id;
            response = await fetch(`${API_URL}/Tecnicos/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        } else {
            response = await fetch(`${API_URL}/Tecnicos`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        }

        if (response.ok) {
            showAlert('tecnicoAlert', id ? 'Técnico actualizado exitosamente' : 'Técnico creado exitosamente', 'success');
            clearTecnicoForm();
            loadTecnicos();
        } else {
            showAlert('tecnicoAlert', 'Error al guardar técnico', 'error');
        }
    } catch (error) {
        showAlert('tecnicoAlert', 'Error: ' + error.message, 'error');
    }
});

async function editTecnico(id) {
    try {
        const response = await fetch(`${API_URL}/Tecnicos/${id}`);
        const tecnico = await response.json();
        
        document.getElementById('tecnicoId').value = tecnico.id;
        document.getElementById('tecnicoNombre').value = tecnico.nombre;
        document.getElementById('tecnicoEspecialidad').value = tecnico.especialidad;
        
        window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (error) {
        showAlert('tecnicoAlert', 'Error al cargar técnico: ' + error.message, 'error');
    }
}

async function deleteTecnico(id) {
    if (!confirm('¿Está seguro de eliminar este técnico?')) return;
    
    try {
        const response = await fetch(`${API_URL}/Tecnicos/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            showAlert('tecnicoAlert', 'Técnico eliminado exitosamente', 'success');
            loadTecnicos();
        } else {
            showAlert('tecnicoAlert', 'Error al eliminar técnico', 'error');
        }
    } catch (error) {
        showAlert('tecnicoAlert', 'Error: ' + error.message, 'error');
    }
}

function clearTecnicoForm() {
    document.getElementById('tecnicoForm').reset();
    document.getElementById('tecnicoId').value = '';
}

// PRUEBAS
async function loadPruebas() {
    try {
        const response = await fetch(`${API_URL}/Pruebas`);
        const pruebas = await response.json();
        
        const table = document.getElementById('pruebasTable');
        if (pruebas.length === 0) {
            table.innerHTML = '<div class="empty-state">No hay pruebas registradas</div>';
            return;
        }

        table.innerHTML = `
            <table>
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Nombre</th>
                        <th>Precio</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${pruebas.map(p => `
                        <tr>
                            <td>${p.codigo}</td>
                            <td>${p.nombre}</td>
                            <td>$${p.precio.toFixed(2)}</td>
                            <td class="actions">
                                <button class="btn btn-warning" onclick="editPrueba('${p.id}')">✏️ Editar</button>
                                <button class="btn btn-danger" onclick="deletePrueba('${p.id}')">🗑️ Eliminar</button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        `;
    } catch (error) {
        showAlert('pruebaAlert', 'Error al cargar pruebas: ' + error.message, 'error');
    }
}

document.getElementById('pruebaForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const id = document.getElementById('pruebaId').value;
    const data = {
        codigo: document.getElementById('pruebaCodigo').value,
        nombre: document.getElementById('pruebaNombre').value,
        precio: parseFloat(document.getElementById('pruebaPrecio').value)
    };

    try {
        let response;
        if (id) {
            data.id = id;
            response = await fetch(`${API_URL}/Pruebas/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        } else {
            response = await fetch(`${API_URL}/Pruebas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        }

        if (response.ok) {
            showAlert('pruebaAlert', id ? 'Prueba actualizada exitosamente' : 'Prueba creada exitosamente', 'success');
            clearPruebaForm();
            loadPruebas();
        } else {
            showAlert('pruebaAlert', 'Error al guardar prueba', 'error');
        }
    } catch (error) {
        showAlert('pruebaAlert', 'Error: ' + error.message, 'error');
    }
});

async function editPrueba(id) {
    try {
        const response = await fetch(`${API_URL}/Pruebas/${id}`);
        const prueba = await response.json();
        
        document.getElementById('pruebaId').value = prueba.id;
        document.getElementById('pruebaCodigo').value = prueba.codigo;
        document.getElementById('pruebaNombre').value = prueba.nombre;
        document.getElementById('pruebaPrecio').value = prueba.precio;
        
        window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (error) {
        showAlert('pruebaAlert', 'Error al cargar prueba: ' + error.message, 'error');
    }
}

async function deletePrueba(id) {
    if (!confirm('¿Está seguro de eliminar esta prueba?')) return;
    
    try {
        const response = await fetch(`${API_URL}/Pruebas/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            showAlert('pruebaAlert', 'Prueba eliminada exitosamente', 'success');
            loadPruebas();
        } else {
            showAlert('pruebaAlert', 'Error al eliminar prueba', 'error');
        }
    } catch (error) {
        showAlert('pruebaAlert', 'Error: ' + error.message, 'error');
    }
}

function clearPruebaForm() {
    document.getElementById('pruebaForm').reset();
    document.getElementById('pruebaId').value = '';
}

// CITAS
async function loadCitaSelects() {
    try {
        const [pacientes, tecnicos, pruebas] = await Promise.all([
            fetch(`${API_URL}/Pacientes`).then(r => r.json()),
            fetch(`${API_URL}/Tecnicos`).then(r => r.json()),
            fetch(`${API_URL}/Pruebas`).then(r => r.json())
        ]);

        const pacienteSelect = document.getElementById('citaPaciente');
        pacienteSelect.innerHTML = '<option value="">Seleccione un paciente</option>' +
            pacientes.map(p => `<option value="${p.id}">${p.nombre} ${p.apellido}</option>`).join('');

        const tecnicoSelect = document.getElementById('citaTecnico');
        tecnicoSelect.innerHTML = '<option value="">Seleccione un técnico</option>' +
            tecnicos.map(t => `<option value="${t.id}">${t.nombre} - ${t.especialidad}</option>`).join('');

        const pruebaSelect = document.getElementById('citaPrueba');
        pruebaSelect.innerHTML = '<option value="">Seleccione una prueba</option>' +
            pruebas.map(p => `<option value="${p.id}">${p.nombre} - $${p.precio.toFixed(2)}</option>`).join('');
    } catch (error) {
        showAlert('citaAlert', 'Error al cargar selects: ' + error.message, 'error');
    }
}

    async function loadCitas() {
    try {
        const response = await fetch(`${API_URL}/Citas`);

        const text = await response.text();
        let citas;

        try {
            citas = JSON.parse(text);
        } catch {
            throw new Error("El backend respondió con un error: " + text);
        }
        
        const table = document.getElementById('citasTable');
        if (citas.length === 0) {
            table.innerHTML = '<div class="empty-state">No hay citas registradas</div>';
            return;
        }

        const [pacientes, tecnicos, pruebas] = await Promise.all([
            fetch(`${API_URL}/Pacientes`).then(r => r.json()),
            fetch(`${API_URL}/Tecnicos`).then(r => r.json()),
            fetch(`${API_URL}/Pruebas`).then(r => r.json())
        ]);


        table.innerHTML = `
            <table>
                <thead>
                    <tr>
                        <th>Paciente</th>
                        <th>Técnico</th>
                        <th>Prueba</th>
                        <th>Fecha</th>
                        <th>Estado</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${citas.map(c => {
                        const paciente = pacientes.find(p => p.id === c.pacienteId);
                        const tecnico = tecnicos.find(t => t.id === c.tecnicoId);
                        const prueba = pruebas.find(p => p.id === c.pruebaId);
                        
                        return `
                            <tr>
                                <td>${paciente ? paciente.nombre + ' ' + paciente.apellido : 'N/A'}</td>
                                <td>${tecnico ? tecnico.nombre : 'N/A'}</td>
                                <td>${prueba ? prueba.nombre : 'N/A'}</td>
                                <td>${new Date(c.fecha).toLocaleString()}</td>
                                <td><span style="padding: 5px 10px; border-radius: 5px; background: ${getEstadoColor(c.estado)}; color: white;">${c.estado}</span></td>
                                <td class="actions">
                                    <button class="btn btn-warning" onclick="editCita('${c.id}')">✏️ Editar</button>
                                    <button class="btn btn-danger" onclick="deleteCita('${c.id}')">🗑️ Eliminar</button>
                                </td>
                            </tr>
                        `;
                    }).join('')}
                </tbody>
            </table>
        `;
    } catch (error) {
        showAlert('citaAlert', 'Error al cargar citas: ' + error.message, 'error');
    }
}

function getEstadoColor(estado) {
    const colors = {
        'Pendiente': '#ffc107',
        'Confirmada': '#17a2b8',
        'Completada': '#28a745',
        'Cancelada': '#dc3545'
    };
    return colors[estado] || '#6c757d';
}

document.getElementById('citaForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const id = document.getElementById('citaId').value;
    const data = {
        pacienteId: document.getElementById('citaPaciente').value,
        tecnicoId: document.getElementById('citaTecnico').value,
        pruebaId: document.getElementById('citaPrueba').value,
        fecha: document.getElementById('citaFecha').value,
        estado: document.getElementById('citaEstado').value
    };

    try {
        let response;
        if (id) {
            data.id = id;
            response = await fetch(`${API_URL}/Citas/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        } else {
            response = await fetch(`${API_URL}/Citas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });
        }

        if (response.ok) {
            showAlert('citaAlert', id ? 'Cita actualizada exitosamente' : 'Cita creada exitosamente', 'success');
            clearCitaForm();
            loadCitas();
        } else {
            const errorText = await response.text();
            showAlert('citaAlert', 'Error al guardar cita: ' + errorText, 'error');
        }
    } catch (error) {
        showAlert('citaAlert', 'Error: ' + error.message, 'error');
    }
});

async function editCita(id) {
    try {
        const response = await fetch(`${API_URL}/Citas/${id}`);
        const cita = await response.json();
        
        document.getElementById('citaId').value = cita.id;
        document.getElementById('citaPaciente').value = cita.pacienteId;
        document.getElementById('citaTecnico').value = cita.tecnicoId;
        document.getElementById('citaPrueba').value = cita.pruebaId;
        
        const fecha = new Date(cita.fecha);
        const fechaLocal = new Date(fecha.getTime() - fecha.getTimezoneOffset() * 60000);
        document.getElementById('citaFecha').value = fechaLocal.toISOString().slice(0, 16);
        
        document.getElementById('citaEstado').value = cita.estado;
        
        window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (error) {
        showAlert('citaAlert', 'Error al cargar cita: ' + error.message, 'error');
    }
}

async function deleteCita(id) {
    if (!confirm('¿Está seguro de eliminar esta cita?')) return;
    
    try {
        const response = await fetch(`${API_URL}/Citas/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            showAlert('citaAlert', 'Cita eliminada exitosamente', 'success');
            loadCitas();
        } else {
            showAlert('citaAlert', 'Error al eliminar cita', 'error');
        }
    } catch (error) {
        showAlert('citaAlert', 'Error: ' + error.message, 'error');
    }
}

function clearCitaForm() {
    document.getElementById('citaForm').reset();
    document.getElementById('citaId').value = '';
}

loadPacientes();