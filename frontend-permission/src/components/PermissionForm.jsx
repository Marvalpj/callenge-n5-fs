import React, { useState } from 'react';

import {
  Grid,
  FormLabel,
  OutlinedInput,
  Button,
  Select,
  MenuItem,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions   
} from '@mui/material';


const PermissionForm = () => {

  const [open, setOpen] = useState(false);
  const [permissionType, setPermissionType] = useState('');

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handlePermissionTypeChange = (event) => {
    setPermissionType(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    // Aquí puedes agregar la lógica para enviar el formulario
    console.log('Formulario enviado:', {
      firstName: event.target.firstName.value,
      lastName: event.target.lastName.value,
      permissionType: event.target.permissionType.value,
    });
  };

  return (
    <Grid container>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={3}>
          <Grid item xs={12} md={6}>
            <FormLabel htmlFor="firstName" required>
              Nombre
            </FormLabel>
            <OutlinedInput
              id="firstName"
              name="firstName"
              type="text"
              placeholder="John"
              autoComplete="given-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={12} md={6}>
            <FormLabel htmlFor="lastName" required>
              Apellido
            </FormLabel>
            <OutlinedInput
              id="lastName"
              name="lastName"
              type="text"
              placeholder="Doe"
              autoComplete="family-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={9}>
            <FormLabel htmlFor="permissionType" required>
              Tipo de Permiso
            </FormLabel>
            <Select
              id="permissionType"
              name="permissionType"
              value={permissionType}
              onChange={handlePermissionTypeChange}
              displayEmpty
              required
              fullWidth
            >
              <MenuItem value="">
                <em>Seleccione un tipo de permiso</em>
              </MenuItem>
              <MenuItem value="read">Lectura</MenuItem>
              <MenuItem value="write">Escritura</MenuItem>
              <MenuItem value="admin">Administrador</MenuItem>
            </Select>
          </Grid>
          <Grid item xs={3}>
            <Button variant="contained" onClick={handleClickOpen}>
              Nuevo Tipo
            </Button>
          </Grid>
          <Grid item xs={12}>
            <Button type="submit" variant="contained" color="primary">
              Enviar
            </Button>
          </Grid>
        </Grid>
      </form>

    {/* Modal para crear nuevo tipo de permiso */}
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Nuevo Tipo de Permiso</DialogTitle>
      <DialogContent>
        <Grid>
            <FormLabel htmlFor="permission-name" required>
              Descripcion del permiso
            </FormLabel>
            <OutlinedInput
              id="permission-name"
              name="permission-name"
              type="text"
              placeholder="Permission"
              required
              fullWidth
            />
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose} color="primary">
          Cancelar
        </Button>
        <Button onClick={handleClose} color="primary">
          Guardar
        </Button>
      </DialogActions>
    </Dialog>

    </Grid>
  );
}

export default PermissionForm;